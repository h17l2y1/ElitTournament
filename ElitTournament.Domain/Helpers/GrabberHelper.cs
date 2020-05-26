using AngleSharp.Dom;
using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ElitTournament.Domain.Helpers
{
	public class GrabberHelper : IGrabberHelper
	{
		private readonly Regex Pattern;
		private readonly List<string> Days;
		private readonly List<string> Places;
		private readonly Dictionary<char, char> _replacements;

		public List<Schedule> ListSchedule { get; set; }
		public List<League> Leagues { get; set; }


		public GrabberHelper()
		{
			Pattern = new Regex("[\t\n]");		
			ListSchedule = new List<Schedule>();
			Leagues = new List<League>();
			_replacements = SetDictionary();

			Days = new List<string>()
			{
				"понедельник", "вторник", "среда", "четверг", "пятница", "суббота", "воскресенье"
			};

			Places = new List<string>()
			{
				"хнурэ", "ХИРЭ", "шервуд", "Football Park", "ШВСМ Пионер","ОНСК Локомотив",
				"СК Звезда", "СК Вирта", "СК Хнувд", "Ск Фарм Академия", "КСК Комунар"
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
				//string ruText = Converter(item.TextContent.ToUpper());
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
					string a = Converter(text);
					ListSchedule.Add(new Schedule(a));
					continue;
				}

				if (index != 0)
				{
					ListSchedule[index - 1].Games.AddRange(text.ToUpper().Split("\n").ToList());
				}

			}
		}

		//private void CreateSchedule2(IElement p)
		//{
		//	if (p.ChildElementCount >= 1)
		//	{
		//		string str = p.Children[0].LocalName;
		//		if (str == "strong")
		//		{
		//			string obj = p.TextContent;
		//			ListSchedule.Add(new Schedule(obj));
		//		}
		//		if (str == "br")
		//		{
		//			var index = ListSchedule.Count();
		//			if (index == 0)
		//			{
		//				return;
		//			}
		//			ListSchedule[index - 1].Games.AddRange(p.TextContent.ToUpper().Split("\n").ToList());
		//		}
		//	}
		//	if (p.ChildElementCount == 0)
		//	{
		//		var index = ListSchedule.Count();
		//		if (index == 0)
		//		{
		//			return;
		//		}
		//		ListSchedule[index - 1].Games.Add(p.TextContent);
		//	}
		//}

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
				Leagues[Leagues.Count - 1].Name = leagueName;
			}

			if (element.LocalName == "strong")
			{
				string leagueName = element.TextContent;

				Leagues.Add(new League());
				Leagues[Leagues.Count - 1].Name = leagueName;
			}

			if (element.LocalName == "table")
			{
				IElement table = element.Children[0];

				foreach (var column in table.Children)
				{
					var tdTeam = column.Children[1];
					string tdName = tdTeam.TextContent;
					string teamName = Pattern
											 .Replace(tdName, "")
											 .Replace("-", " ")
											 .ToUpper();

					Leagues[Leagues.Count - 1].Teams.Add(teamName);
				}
				Leagues[Leagues.Count - 1].Teams.RemoveAt(0);
			}
		}

		private string Converter(string str)
		{
			var sb = new StringBuilder(str);
			foreach (var kvp in _replacements)
			{
				sb.Replace(kvp.Key, kvp.Value);
			}
			return sb.ToString();
		}

		private Dictionary<char, char> SetDictionary()
		{
			Dictionary<char, char> Replacements = new Dictionary<char, char>()
			{
				['a'] = 'а',
				['A'] = 'А',
				['B'] = 'В',
				['c'] = 'с',
				['C'] = 'С',
				['e'] = 'е',
				['E'] = 'Е',
				['H'] = 'Н',
				['i'] = 'і',
				['I'] = 'І',
			};
			return Replacements;
		}

	}

}
