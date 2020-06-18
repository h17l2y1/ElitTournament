using ElitTournament.DAL.Config;
using ElitTournament.DAL.Entities;
using ElitTournament.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.DAL.Repositories
{
	public class DataVersionRepository : IDataVersionRepository
	{
		private ApplicationContext _context { get; set; }

		protected DbSet<DataVersion> _dbSet { get; set; }

		public DataVersionRepository(ApplicationContext context)
		{
			_context = context;
			_dbSet = _context.Set<DataVersion>();
		}

		public async Task CreateAsync(DataVersion entity)
		{
			await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task CreateAsync(IEnumerable<DataVersion> collection)
		{
			await _dbSet.AddRangeAsync(collection);
			await _context.SaveChangesAsync();
		}

		public async Task<int> GetLastVersion()
		{
			var version = await _dbSet.LastOrDefaultAsync();
			return version.Version;
		}
	}
}
