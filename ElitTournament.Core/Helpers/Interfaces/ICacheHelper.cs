using ElitTournament.DAL.Entities;
using System.Collections.Generic;

namespace ElitTournament.Core.Helpers.Interfaces
{
	public interface ICacheHelper
	{
		bool IsCacheExist { get; set; }

		void Update(List<Schedule> schedule, List<League> league);

		List<League> GetLeagues();

		List<Schedule> GetSchedule();

		List<string> GetTeams();

		string FindGame(string teamName);
	}
}
