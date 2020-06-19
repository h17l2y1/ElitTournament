using AngleSharp.Dom;
using ElitTournament.DAL.Entities;
using ElitTournament.Core.Helpers.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ElitTournament.DAL.Repositories.Interfaces;

namespace ElitTournament.Core.Helpers
{
	public class GrabberHelper : IGrabberHelper
	{
		private readonly Regex _pattern;
		private readonly List<string> _days;
		private readonly List<string> _places;
		private readonly IImageHelper _imageHelper;

		public List<Schedule> ListSchedule { get; set; }
		public List<League> Leagues { get; set; }
		public List<League> Tables { get; set; }
		
		public GrabberHelper(IImageHelper imageHelper)
		{
			_imageHelper = imageHelper;
			_pattern = new Regex("[\t\n]");		
			ListSchedule = new List<Schedule>();
			Leagues = new List<League>();
			Tables = new List<League>();

			_days = new List<string>()
			{
				"понедельник", "вторник", "среда", "четверг", "пятница", "суббота", "воскресенье",	// rus
				"Bторник", "Cреда", "Cуббота", "Воскресенье"										// eng + rus
			};

			_places = new List<string>()
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

			// TODO: refactor
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

		public List<League> ParseTables(IDocument document)
		{
			IHtmlCollection<IElement> listTables = Parse(document);

			foreach (var item in listTables)
			{
				CreateTable(item);
			}

			return Tables;
		}
		
		private void CreateSchedule(List<string> listP)
		{
			foreach (var text in listP)
			{
				bool isDateExist = false;
				bool isPlaceExist = false;
				int index = ListSchedule.Count();

				foreach (var day in _days)
				{
					if (!isDateExist)
					{
						isDateExist = text.Contains(day.ToUpper());
					}
				}

				foreach (var place in _places)
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

			IEnumerable<string> links = Enumerable.Range(0, ul.Count())
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
					string teamName = _pattern.Replace(tdName, "")
											 .Replace("-", " ")
											 .ToUpper();

					Leagues[Leagues.Count - 1].Teams.Add(new Team(teamName));
				}
				Leagues[Leagues.Count - 1].Teams.RemoveAt(0);
			}
		}

		private void CreateTable(IElement element)
		{
			if (element.TagName == "P")
			{
				var c = element.Children;
				foreach (var tt in c)
				{
					if (tt.TagName == "STRONG")
					{
						string leagueName = tt.TextContent.Trim();
						Tables.Add(new League());
						Tables[Tables.Count - 1].Name = leagueName.Trim();
					}
				}
			}

			if (element.TagName == "STRONG")
			{
				string leagueName = element.TextContent;

				Tables.Add(new League());
				Tables[Tables.Count - 1].Name = leagueName.Trim();
			}
			
			if (element.TagName == "TABLE")
			{
				if (Tables.Count > 0)
				{
					string tableName = Tables[Tables.Count - 1].Name;
					_imageHelper.CreateImage(element.OuterHtml, tableName);
				}
				
				var tempElement = element.Children;
				var table = tempElement.FirstOrDefault();
				
				List<Team> teams = table.Children.Skip(1)
												 .Select(tr => new Team
												 {
													 Position = int.Parse(tr.Children[0].TextContent),
													 Name = tr.Children[2].TextContent.Trim(),
													 Played = int.Parse(tr.Children[3].TextContent),
													 Won = int.Parse(tr.Children[4].TextContent),
													 Drawn = int.Parse(tr.Children[5].TextContent),
													 Lost = int.Parse(tr.Children[6].TextContent),
													 Goals = int.Parse(tr.Children[7].TextContent),
													 GoalDifference = int.Parse(tr.Children[8].TextContent),
													 Points = int.Parse(tr.Children[9].TextContent)
												 })
												 .ToList();

				if (Tables.Count > 0)
				{
					Tables[Tables.Count - 1].Teams = teams;
				}
			}
		}

	}

}
