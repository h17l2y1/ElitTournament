using AngleSharp.Dom;
using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Providers.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Providers
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

			List<Schedule> result = new List<Schedule>();
			//foreach (var link in links)
			//{
			//	IDocument document = await GetPage(link);
			//	result = _schedule.Parse(document);
			//}
			var test = links.ToList()[1];
			IDocument document = await GetPage(test);
			result = _grabber.ParseSchedule(document);

			return result;
		}

		private async Task<IEnumerable<string>> GetLinks()
		{
			IDocument document = await GetPage(ScheduleUrl);
			IEnumerable<string> links = _grabber.GetLinks(document);
			return links;
		}

		public async Task<List<League>> GetLeagues()
		{
			IDocument document = await GetPage(ScoreUrl);
			List<League> leagues = _grabber.ParseLeagues(document);

			return leagues;
		}
	}
}
