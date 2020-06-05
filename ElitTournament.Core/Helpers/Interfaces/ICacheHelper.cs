using ElitTournament.Core.Entities;
using System.Collections.Generic;

namespace ElitTournament.Core.Helpers.Interfaces
{
	public interface ICacheHelper
	{
		void Update(List<Schedule> schedule, List<League> league);

		List<League> GetLeagues();

		List<Schedule> GetSchedule();

		List<string> GetTeams();

		List<string> FindGame(string teamName);
	}
}
