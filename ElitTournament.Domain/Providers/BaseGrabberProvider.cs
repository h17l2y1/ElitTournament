using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Providers.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Providers
{
	public class BaseGrabberProvider : IBaseGrabberProvider
	{
		protected readonly IHtmlLoaderHelper _htmlLoaderHelper;
		protected readonly IConfiguration _сonfiguration;

		protected readonly HtmlParser domParser;

		public BaseGrabberProvider(IHtmlLoaderHelper htmlLoaderHelper, IConfiguration сonfiguration)
		{
			_htmlLoaderHelper = htmlLoaderHelper;
			_сonfiguration = сonfiguration;
			domParser = new HtmlParser();
		}

		public string ScheduleUrl { get; set; }

		public string ScoreUrl { get; set; }

		public async Task<IDocument> GetPage(string url)
		{
			IHtmlDocument source = await _htmlLoaderHelper.GetPageSource(url);
			IDocument document = await domParser.ParseDocumentAsync(source);
			return document;
		}
	}
}
