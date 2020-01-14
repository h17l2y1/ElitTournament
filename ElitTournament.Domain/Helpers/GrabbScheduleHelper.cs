using AngleSharp.Dom;
using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ElitTournament.Domain.Helpers
{
	public class GrabbScheduleHelper : BaseGrabberHelper, IGrabbScheduleHelper
	{
		public List<Schedule> ListSchedule { get; set; }

		public GrabbScheduleHelper()
		{
			ListSchedule = new List<Schedule>();
		}

		public List<Schedule> Parse(IDocument document)
		{
			IHtmlCollection<IElement> iElementList = document.QuerySelectorAll("div.entry-content");

			IHtmlCollection<IElement> listP = iElementList[0].Children;

			int count = listP.Count();

			Enumerable.Range(0, count)
					  .Select(i => CreateProduct(listP[i]))
					  .ToList();

			return ListSchedule;
		}

		private Schedule CreateProduct(IElement p)
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

			return null;
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

	}

}
