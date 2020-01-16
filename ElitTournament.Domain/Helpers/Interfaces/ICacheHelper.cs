using ElitTournament.Domain.Entities;
using System.Collections.Generic;

namespace ElitTournament.Domain.Helpers.Interfaces
{
	public interface ICacheHelper
	{
		void Update(List<Schedule> schedule, List<League> league);

		List<string> FindGame(string teamName);

		List<League> GetLeagues();
	}
}
