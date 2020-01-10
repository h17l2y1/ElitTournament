using AngleSharp.Dom;
using ElitTournament.Domain.Helpers.Interfaces;

namespace ElitTournament.Domain.Helpers
{
	public class BaseHelper : IBaseHelper
	{
		public string GetData(IElement iElement, string selector)
		{
			IElement currentIElement = iElement.QuerySelector(selector);
			if (currentIElement == null)
			{
				return null;
			}

			string result = currentIElement.Text();
			result = result.Replace("\n", " ").Trim();
			if (result.Length < 1)
			{
				return null;
			}
			return result;
		}

		public string GetData(IElement iElement, string selector, string attribute)
		{
			IElement currentIElement = iElement.QuerySelector(selector);
			if (currentIElement == null)
			{
				return null;
			}
			string result = currentIElement.GetAttribute(attribute);
			result = result.Replace("\n", " ").Trim();
			if (result.Length < 1)
			{
				return null;
			}
			return result;
		}
	}
}
