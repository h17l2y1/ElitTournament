using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using ElitTournament.Domain.Helpers.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Helpers
{
	public class HtmlLoaderHelper : IHtmlLoaderHelper
	{
		readonly HttpClient client;

		public HtmlLoaderHelper()
		{
			client = new HttpClient();
		}

		public async Task<IHtmlDocument> GetPageSource(string url)
		{
			var domParser = new HtmlParser();

			var source = await GetaPageSource(url);
			var document = await domParser.ParseDocumentAsync(source);
			return document;
		}

		private async Task<string> GetaPageSource(string url)
		{
			var response = await client.GetAsync(url);
			string source = null;

			if (response != null && response.StatusCode == HttpStatusCode.OK)
			{
				source = await response.Content.ReadAsStringAsync();
			}
			return source;
		}

	}
}
