using AngleSharp.Dom;
using ElitTournament.DAL.Entities;
using ElitTournament.Core.Helpers.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ElitTournament.DAL.Repositories.Interfaces;
using ElitTournament.DAL.Enities;

namespace ElitTournament.Core.Helpers
{
	public class GrabberHelper : IGrabberHelper
	{
		private readonly IScheduleRepository _scheduleRepository;
		private readonly IGameRepository _gameRepository;
		private readonly Regex Pattern;
		private readonly List<string> Days;
		private readonly List<string> Places;

		public List<Schedule> ListSchedule { get; set; }
		public List<League> Leagues { get; set; }

		public GrabberHelper(IScheduleRepository scheduleRepository, IGameRepository gameRepository)
		{
			_scheduleRepository = scheduleRepository;
			_gameRepository = gameRepository;


			Pattern = new Regex("[\t\n]");		
			ListSchedule = new List<Schedule>();
			Leagues = new List<League>();

			Days = new List<string>()
			{
				"понедельник", "вторник", "среда", "четверг", "пятница", "суббота", "воскресенье",	// rus
				"Bторник", "Cреда", "Cуббота", "Воскресенье"										// eng + rus
			};

			Places = new List<string>()
			{
				"хнурэ", "ХИРЭ", "шервуд", "Football Park", "ШВСМ Пионер","ОНСК Локомотив",
				"СК Звезда", "СК Вирта", "СК Хнувд", "Ск Фарм Академия", "КСК Комунар", "ФармАкадемия"
			};
		}

		public IHtmlCollection<IElement> Parse(IDocument document)
		{
			IHtmlCollection<IElement> iElementList = document.QuerySelectorAll("div.entry-content");

			IHtmlCollection<IElement> result = iElementList[0].Children;

			return result;
		}

		public List<Schedule> ParseSchedule(IDocument document)
		{
			IHtmlCollection<IElement> listP = Parse(document);

			var test = new List<string>();

			foreach (var item in listP)
			{
				test.Add(item.TextContent.ToUpper());
			}

			CreateSchedule(test);

			return ListSchedule;
		}

		public List<League> ParseLeagues(IDocument document)
		{
			IHtmlCollection<IElement> listTables = Parse(document);

			foreach (var item in listTables)
			{
				CreateLeague(item);
			}

			return Leagues;
		}

		private void CreateSchedule(List<string> listP)
		{
			foreach (var text in listP)
			{
				bool isDateExist = false;
				bool isPlaceExist = false;
				int index = ListSchedule.Count();

				foreach (var day in Days)
				{
					if (!isDateExist)
					{
						isDateExist = text.Contains(day.ToUpper());
					}
				}

				foreach (var place in Places)
				{
					if (!isPlaceExist)
					{
						isPlaceExist = text.Contains(place.ToUpper());
					}
				}

				if (isDateExist && isPlaceExist)
				{
					ListSchedule.Add(new Schedule(text));
					continue;
				}

				if (index != 0)
				{
					if (!text.ToUpper().Contains("ПОЛЕ"))
					{
						ListSchedule[index - 1].Games.AddRange(text.ToUpper()
																	.Split("\n")
																	.Select(x => new Game(x)));
					}
				}

			}
		}

		public IEnumerable<string> GetLinks(IDocument document)
		{
			IHtmlCollection<IElement> iElementList = document.QuerySelectorAll("ul.lcp_catlist");

			IHtmlCollection<IElement> ul = iElementList[0].Children;

			IEnumerable<string> links = Enumerable
				.Range(0, ul.Count())
				.Select(i => GetLink(ul[i]));
			return links;
		}

		private string GetLink(IElement ul)
		{
			IElement iElement = ul.Children[0];
			string link = iElement.GetAttribute("href");
			return link;
		}

		private void CreateLeague(IElement element)
		{
			if (element.ChildElementCount > 0 && element.Children[0].LocalName == "strong")
			{
				var c2 = element.Children[0];
				string leagueName = c2.TextContent;

				Leagues.Add(new League());
				Leagues[Leagues.Count - 1].Name = leagueName.Trim();
			}

			if (element.LocalName == "strong")
			{
				string leagueName = element.TextContent;

				Leagues.Add(new League());
				Leagues[Leagues.Count - 1].Name = leagueName.Trim();
			}

			if (element.LocalName == "table")
			{
				IElement table = element.Children[0];

				foreach (var column in table.Children)
				{
					var tdTeam = column.Children[1];
					string tdName = tdTeam.TextContent;
					string teamName = Pattern.Replace(tdName, "")
											 .Replace("-", " ")
											 .ToUpper();

					Leagues[Leagues.Count - 1].Teams.Add(new Team(teamName));
				}
				Leagues[Leagues.Count - 1].Teams.RemoveAt(0);
			}
		}

	}

}
