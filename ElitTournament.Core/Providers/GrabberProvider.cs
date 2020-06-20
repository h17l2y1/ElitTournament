using AngleSharp.Dom;
using ElitTournament.Core.Providers.Interfaces;
using ElitTournament.Core.Helpers.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElitTournament.DAL.Entities;

namespace ElitTournament.Core.Providers
{
	public class GrabberProvider : BaseGrabberProvider, IGrabberProvider
	{
		private readonly IGrabberHelper _grabber;

		public GrabberProvider(IHtmlLoaderHelper htmlLoaderHelper, IConfiguration сonfiguration, IGrabberHelper grabber)
			: base(htmlLoaderHelper, сonfiguration)
		{
			ScheduleUrl = _сonfiguration.GetSection("ElitTournament:Schedule").Value;
			ScoreUrl = _сonfiguration.GetSection("ElitTournament:Score").Value;
			TableUrl = _сonfiguration.GetSection("ElitTournament:Table").Value;
			_grabber = grabber;
		}

		public async Task<IEnumerable<Schedule>> GetSchedule()
		{
			IEnumerable<string> links = await GetLinks();
			string lastStageLink = links.FirstOrDefault(x=>x.Contains("raspisanie"));
			IDocument document = await GetPage(lastStageLink);
			IEnumerable<Schedule> result = _grabber.ParseSchedule(document);

			return result;
		}

		public Task<IEnumerable<League>> GetScores()
		{
			// 	IDocument document = await GetPage(ScoreUrl);
			// 	List<League> leagues = _grabber.ParseLeagues(document);
			// 	return leagues;
			throw new System.NotImplementedException();
		}

		public async Task<IEnumerable<League>> GetLeagues()
		{
			IDocument document = await GetPage(TableUrl);
			IEnumerable<League> result = await _grabber.ParseTables(document);
			return result;
		}
		
		private async Task<IEnumerable<string>> GetLinks()
		{
			IDocument document = await GetPage(ScheduleUrl);
			IEnumerable<string> links = _grabber.GetLinks(document);
			return links;
		}
	}
}
