using ElitTournament.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.DAL.Repositories.Interfaces
{
	public interface ILeagueRepository : IBaseRepository<League>
	{
		Task<IEnumerable<League>> GetAll(int version);
	}
}
