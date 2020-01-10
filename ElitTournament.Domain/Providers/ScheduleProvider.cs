using AngleSharp.Dom;
using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Providers.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Providers
{
	public class ScheduleProvider : BaseProvider, IScheduleProvider
	{
		protected readonly IScheduleHelper _schedule;

		public ScheduleProvider(IHtmlLoaderHelper htmlLoaderHelper, IConfiguration сonfiguration, IScheduleHelper schedule)
			: base(htmlLoaderHelper, сonfiguration)
		{
			ScheduleUrl = _сonfiguration.GetSection("ElitTournament:Schedule").Value;

			_schedule = schedule;
		}

		public async Task<List<Schedule>> GetSchedule()
		{
			IDocument document = await GetPage(ScheduleUrl);
			List<Schedule> result = _schedule.Parse(document);
			return result;
		}




	}
}
