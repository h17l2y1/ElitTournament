using AngleSharp.Dom;
using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Providers.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Providers
{
	public class GrabbScoreProvider : BaseGrabberProvider, IGrabbScoreProvider
	{
		protected readonly IGrabbScoreHelper _score;

		public GrabbScoreProvider(IHtmlLoaderHelper htmlLoaderHelper, IConfiguration сonfiguration, IGrabbScoreHelper score)
			: base(htmlLoaderHelper, сonfiguration)
		{
			ScoreUrl = _сonfiguration.GetSection("ElitTournament:Score").Value;
			_score = score;
		}

		public async Task<List<League>> GetLeague()
		{
			IDocument document = await GetPage(ScoreUrl);
			List<League> leagues = _score.Parse(document);

			return leagues;
		}

	}
}
