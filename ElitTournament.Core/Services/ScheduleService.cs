using ElitTournament.Core.Entities;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Core.Providers.Interfaces;
using ElitTournament.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Core.Services
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
			//List<string> result1 = _сacheHelper.FindGame(teamName);
			//var result = String.Join(", ", result1.ToArray());
			//return result;
			return null;
		}

		public async Task<List<League>> GetLeagues()
		{
			List<League> league = await _grabberProvider.GetLeagues();
			return league;
		}
	}
}
