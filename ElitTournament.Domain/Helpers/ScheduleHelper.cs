using AngleSharp.Dom;
using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Helpers
{
	public class ScheduleHelper : BaseHelper, IScheduleHelper
	{
		public List<Schedule> Parse(IDocument document)
		{
			IHtmlCollection<IElement> iElementList = document.QuerySelectorAll("ul.lcp_catlist");

			List<Schedule> products = Enumerable
				.Range(0, iElementList.Count())
				.Select(i => CreateProduct(iElementList[i]))
				.ToList();

			return products;
		}

		private Schedule CreateProduct(IElement td)
		{
			//IElement imgElement = td.QuerySelector("a.product-img");
			//IElement nameElemnt = td.QuerySelector("div.product-title");

			//string desc = GetData(td, "div.product-description");
			//string priceStr = GetData(td, "div.price");

			return null;
		}

	}
}
