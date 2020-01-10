using AngleSharp.Dom;

namespace ElitTournament.Domain.Helpers.Interfaces
{
	public interface IBaseHelper
	{
		string GetData(IElement iElement, string selector);

		string GetData(IElement iElement, string selector, string attribute);
	}
}
