using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Core.Providers.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ElitTournament.Core.Providers
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
		
		public string TableUrl { get; set; }

		public async Task<IDocument> GetPage(string url)
		{
			IHtmlDocument source = await _htmlLoaderHelper.GetPageSource(url);
			IDocument document = await domParser.ParseDocumentAsync(source);
			return document;
		}
	}
}
