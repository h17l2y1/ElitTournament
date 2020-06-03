using ElitTournament.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Core.Providers.Interfaces
{
	public interface IGrabberProvider
	{
		Task<List<Schedule>> GetSchedule();

		Task<List<League>> GetLeagues();
	}
}
