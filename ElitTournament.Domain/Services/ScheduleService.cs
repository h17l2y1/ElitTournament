using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Providers.Interfaces;
using ElitTournament.Domain.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Services
{
	public class ScheduleService : IScheduleService
	{
		private readonly ICacheHelper _сacheHelper;
		private readonly IGrabberProvider _grabberProvider;

		public ScheduleService(ICacheHelper сacheHelper, IGrabberProvider grabbScoreProvider)
		{
			_сacheHelper = сacheHelper;
			_grabberProvider = grabbScoreProvider;
		}

		public string FindGame(string teamName)
		{
			string result = _сacheHelper.FindGame(teamName);
			return result;
		}

		public async Task<List<League>> GetLeagues()
		{
			List<League> league = await _grabberProvider.GetLeagues();
			return league;
		}
	}
}
