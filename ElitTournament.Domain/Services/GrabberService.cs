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
		private readonly IGrabberProvider _grabberProvider;
		private readonly ICacheHelper _сacheHelper;

		public GrabberService(IGrabberProvider scheduleProvider, ICacheHelper сacheHelper)
		{
			_grabberProvider = scheduleProvider;
			_сacheHelper = сacheHelper;
		}

		public async Task GrabbElitTournament()
		{
			List<Schedule> schedule = await _grabberProvider.GetSchedule();
			List<League> leagues = await _grabberProvider.GetLeagues();

			_сacheHelper.Update(schedule, leagues);
		}

	}
}
