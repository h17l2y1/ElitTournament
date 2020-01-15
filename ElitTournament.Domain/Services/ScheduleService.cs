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
		private readonly IGrabbScoreProvider _grabbScoreProvider;

		public ScheduleService(ICacheHelper сacheHelper, IGrabbScoreProvider grabbScoreProvider)
		{
			_сacheHelper = сacheHelper;
			_grabbScoreProvider = grabbScoreProvider;
		}

		public string FindGame(string teamName)
		{
			List<Schedule> schedule = _сacheHelper.Get();
			if (schedule == null)
			{
				return "Cache is empty";
			}

			foreach (var place in schedule)
			{
				string a = place.Place;
				foreach (var game in place.Games)
				{
					if (game.Contains(teamName))
					{
						return $"{place.Place} {game}";
					}
				}
			}
			return "Команда не найдена";
		}

		public async Task<List<League>> GetLeagues()
		{
			List<League> league = await _grabbScoreProvider.GetLeague();
			return league;
		}
	}
}
