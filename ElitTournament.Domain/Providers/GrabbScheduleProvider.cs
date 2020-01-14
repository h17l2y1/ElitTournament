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
	public class GrabbScheduleProvider : BaseGrabberProvider, IGrabbScheduleProvider
	{
		protected readonly IGrabbScheduleHelper _schedule;

		public GrabbScheduleProvider(IHtmlLoaderHelper htmlLoaderHelper, IConfiguration сonfiguration, IGrabbScheduleHelper schedule)
			: base(htmlLoaderHelper, сonfiguration)
		{
			ScheduleUrl = _сonfiguration.GetSection("ElitTournament:Schedule").Value;

			_schedule = schedule;
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
			var test = links.ToList()[0];
			IDocument document = await GetPage(test);
			result = _schedule.Parse(document);

			return result;
		}

		private async Task<IEnumerable<string>> GetLinks()
		{
			IDocument document = await GetPage(ScheduleUrl);
			IEnumerable<string> links = _schedule.GetLinks(document);
			return links;
		}


	}
}
