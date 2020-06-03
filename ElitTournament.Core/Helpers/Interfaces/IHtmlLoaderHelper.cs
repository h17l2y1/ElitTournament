using AngleSharp.Html.Dom;
using System.Threading.Tasks;

namespace ElitTournament.Core.Helpers.Interfaces
{
	public interface IHtmlLoaderHelper
	{
		Task<IHtmlDocument> GetPageSource(string url);
	}
}
