using ElitTournament.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Core.Providers.Interfaces
{
	public interface IGrabberProvider
	{
		Task<IEnumerable<Schedule>> GetSchedule();

		Task<IEnumerable<League>> GetScores();

		Task<IEnumerable<League>> GetLeagues();
	}
}
