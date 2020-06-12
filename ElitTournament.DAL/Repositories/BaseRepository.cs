using ElitTournament.DAL.Config;
using ElitTournament.DAL.Entities.Interfaces;
using ElitTournament.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElitTournament.DAL.Repositories
{
	public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity
	{
		private ApplicationContext _сontext { get; set; }

		protected DbSet<TEntity> _dbSet { get; set; }

		public BaseRepository(ApplicationContext context)
		{
			_сontext = context;
			_dbSet = _сontext.Set<TEntity>();
		}

		public virtual async Task<TEntity> GetByIdAsync(int id)
		{
			return await _dbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
		}

		public virtual async Task<IEnumerable<TEntity>> GetAll()
		{
			return await GetAllHelper().ToListAsync();
		}

		public virtual async Task CreateAsync(TEntity entity)
		{
			await _dbSet.AddAsync(entity);
			await _сontext.SaveChangesAsync();
		}

		public virtual async Task CreateAsync(IEnumerable<TEntity> collection)
		{
			await _dbSet.AddRangeAsync(collection);
			await _сontext.SaveChangesAsync();
		}

		public virtual async Task RemoveByIdAsync(int id)
		{
			var entity = await GetByIdAsync(id);
			_dbSet.Remove(entity);
			await _сontext.SaveChangesAsync();
		}

		public virtual async Task RemoveAsync(TEntity entity)
		{
			_dbSet.Remove(entity);
			await _сontext.SaveChangesAsync();
		}

		public virtual async Task Update(TEntity entity)
		{
			_dbSet.Update(entity);
			await _сontext.SaveChangesAsync();
		}

		protected IQueryable<TEntity> GetAllHelper()
		{
			return _dbSet.AsNoTracking();
		}

	}
}
