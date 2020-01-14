using AngleSharp.Dom;
using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Providers.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Providers
{
	public class ScoreProvider : BaseGrabberProvider, IScoreProvider
	{
		protected readonly IScoreHelper _score;

		public ScoreProvider(IHtmlLoaderHelper htmlLoaderHelper, IConfiguration сonfiguration, IScoreHelper score)
			: base(htmlLoaderHelper, сonfiguration)
		{
			ScoreUrl = _сonfiguration.GetSection("ElitTournament:Score").Value;

			_score = score;
		}

		public async Task<IDocument> GetScoreIDocument()
		{
			IDocument document = await GetPage(ScoreUrl);
			return document;
		}




	}
}
