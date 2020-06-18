using ElitTournament.DAL.Entities;
using ElitTournament.DAL.Config;
using ElitTournament.DAL.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ElitTournament.DAL.Repositories
{
	public class LeagueRepository : BaseRepository<League>, ILeagueRepository
	{
		public LeagueRepository(ApplicationContext context) : base(context)
		{
		}

		public async Task<IEnumerable<League>> GetAll(int version)
		{
			IEnumerable<League> leagues = await _dbSet.Include(x => x.Teams)
													  .Where(e=>e.DataVersionId == version)
													  .OrderBy(o => o.CreationDate)
													  .AsNoTracking()
													  .ToListAsync();
			return leagues;
		}
	}
}
