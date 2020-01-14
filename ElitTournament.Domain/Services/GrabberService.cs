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

		public async Task<string> GrabbSchedule()
		{
			List<Schedule> result = await _scheduleProvider.GetSchedule();
			_сacheHelper.SaveSchedule(result);

			return $"Grabbed {result.Count} places";
		}

		public async Task<string> UpdateSchedule()
		{
			List<Schedule> result = await _scheduleProvider.GetSchedule();
			_сacheHelper.Update(result);

			return $"Cache cleaned. Grabbed {result.Count} places";
		}

		public async Task<List<League>> GrabbLeagues()
		{
			List<League> leagues = await _scoreProvider.GetLeague();


			return leagues;
		}
	}
}
