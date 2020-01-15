using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Providers.Interfaces;
using ElitTournament.Domain.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Services
{
	public class GrabberService : IGrabberService
	{
		private readonly IGrabbScheduleProvider _scheduleProvider;
		private readonly IGrabbScoreProvider _scoreProvider;
		private readonly ICacheHelper _сacheHelper;

		public GrabberService(IGrabbScheduleProvider scheduleProvider, IGrabbScoreProvider scoreProvider, ICacheHelper сacheHelper)
		{
			_scoreProvider = scoreProvider;
			_scheduleProvider = scheduleProvider;
			_сacheHelper = сacheHelper;
		}

		public async Task<string> GrabbElitTournament()
		{
			List<Schedule> schedule = await _scheduleProvider.GetSchedule();
			List<League> leagues = await _scoreProvider.GetLeague();

			_сacheHelper.Update(schedule, leagues);
			return $"Grabbed {leagues.Count} leagues and {schedule.Count} places";
		}

	}
}
