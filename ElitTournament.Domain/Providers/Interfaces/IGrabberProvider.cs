using ElitTournament.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Providers.Interfaces
{
	public interface IGrabberProvider
	{
		Task<List<Schedule>> GetSchedule();

		Task<List<League>> GetLeagues();
	}
}
