using ElitTournament.DAL.Entities;
using ElitTournament.DAL.Config;
using ElitTournament.DAL.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ElitTournament.DAL.Repositories
{
	public class LeagueRepository : BaseRepository<League>, ILeagueRepository
	{
		public LeagueRepository(ApplicationContext context) : base(context)
		{
		}

		public async override Task<IEnumerable<League>> GetAll()
		{
			IEnumerable<League> leagues = await _dbSet.Include(x => x.Teams).AsNoTracking().ToListAsync();
			return leagues;
		}
	}
}
