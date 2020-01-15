using AngleSharp.Dom;
using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ElitTournament.Domain.Helpers
{
	public class GrabberHelper : IGrabberHelper
	{
		public List<Schedule> ListSchedule { get; set; }
		public List<League> Leagues { get; set; }

		private readonly Regex Pattern;

		public GrabberHelper()
		{
			ListSchedule = new List<Schedule>();
			Leagues = new List<League>();
			Pattern = new Regex("[\t\n]");
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

			foreach (var item in listP)
			{
				CreateProduct(item);
			}

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

		private void CreateProduct(IElement p)
		{
			if (p.ChildElementCount >= 1)
			{
				string str = p.Children[0].LocalName;
				if (str == "strong")
				{
					string obj = p.TextContent;
					ListSchedule.Add(new Schedule(obj));
				}
				if (str == "br")
				{
					var index = ListSchedule.Count();
					ListSchedule[index - 1].Games = p.TextContent.Split("\n").ToList();
				}
			}
			if (p.ChildElementCount == 0)
			{
				var index = ListSchedule.Count();
				ListSchedule[index - 1].Games.Add(p.TextContent);
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
					string teamName = Pattern.Replace(tdName, "");

					Leagues[Leagues.Count - 1].Teams.Add(teamName);
				}
				Leagues[Leagues.Count - 1].Teams.RemoveAt(0);
			}
		}
	}

}
