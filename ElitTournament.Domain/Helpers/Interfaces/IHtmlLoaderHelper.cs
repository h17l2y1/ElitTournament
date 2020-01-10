using AngleSharp.Html.Dom;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Helpers.Interfaces
{
	public interface IHtmlLoaderHelper
	{
		Task<IHtmlDocument> GetPageSource(string url);
	}
}
