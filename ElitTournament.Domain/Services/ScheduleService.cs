using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Providers.Interfaces;
using ElitTournament.Domain.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Services
{
	public class ScheduleService : IScheduleService
	{
		private readonly IScheduleProvider _scheduleProvider;

		public ScheduleService(IScheduleProvider scheduleProvider)
		{
			_scheduleProvider = scheduleProvider;
		}

		public async Task GetSchedule()
		{
			List<Schedule> result = await _scheduleProvider.GetSchedule();

			//return null;
		}
	}
}
