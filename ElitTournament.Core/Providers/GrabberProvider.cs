using AngleSharp.Dom;
using ElitTournament.Core.Providers.Interfaces;
using ElitTournament.Core.Entities;
using ElitTournament.Core.Helpers.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElitTournament.Core.Providers
{
	public class GrabberProvider : BaseGrabberProvider, IGrabberProvider
	{
		protected readonly IGrabberHelper _grabber;

		public GrabberProvider(IHtmlLoaderHelper htmlLoaderHelper, IConfiguration сonfiguration, IGrabberHelper schedule)
			: base(htmlLoaderHelper, сonfiguration)
		{
			ScheduleUrl = _сonfiguration.GetSection("ElitTournament:Schedule").Value;
			ScoreUrl = _сonfiguration.GetSection("ElitTournament:Score").Value;
			_grabber = schedule;
		}

		public async Task<List<Schedule>> GetSchedule()
		{
			IEnumerable<string> links = await GetLinks();
			string lastStageLink = links.FirstOrDefault(x=>x.Contains("raspisanie"));
			IDocument document = await GetPage(lastStageLink);
			List<Schedule> result = _grabber.ParseSchedule(document);

			return result;
		}

		public async Task<List<League>> GetLeagues()
		{
			IDocument document = await GetPage(ScoreUrl);
			List<League> leagues = _grabber.ParseLeagues(document);
			return leagues;
		}

		private async Task<IEnumerable<string>> GetLinks()
		{
			IDocument document = await GetPage(ScheduleUrl);
			IEnumerable<string> links = _grabber.GetLinks(document);
			return links;
		}
	}
}
