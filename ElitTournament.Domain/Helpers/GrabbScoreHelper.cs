using AngleSharp.Dom;
using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ElitTournament.Domain.Helpers
{
	public class GrabbScoreHelper : BaseGrabberHelper, IGrabbScoreHelper
	{
		public List<League> Leagues { get; set; }

		private readonly Regex Pattern;

		public GrabbScoreHelper()
		{
			Leagues = new List<League>();
			Pattern = new Regex("[\t\n]");
		}

		public List<League> Parse(IDocument document)
		{
			IHtmlCollection<IElement> iElementList = document.QuerySelectorAll("div.entry-content");

			IHtmlCollection<IElement> listTables = iElementList[0].Children;

			foreach (var item in listTables)
			{
				CreateLeague(item);
			}

			return Leagues;
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
