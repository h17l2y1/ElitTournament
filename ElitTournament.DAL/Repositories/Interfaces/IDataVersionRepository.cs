using ElitTournament.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.DAL.Repositories.Interfaces
{
	public interface IDataVersionRepository
	{
		Task CreateAsync(DataVersion entity);

		Task CreateAsync(IEnumerable<DataVersion> collection);

		Task<int> GetLastVersion();
	}
}
