using ElitTournament.Core.Entities;
using ElitTournament.DAL.Config;
using ElitTournament.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.DAL.Repositories
{
	public class UserRepository : IUserRepository
	{
		protected ApplicationContext _context;

		public UserRepository(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<User> Get(int id)
		{
			return await _context.Set<User>().FindAsync(id);
		}

		public async Task<bool> IsExist(string clientId)
		{
			User user = await _context.Set<User>().SingleOrDefaultAsync(p=>p.ClientId == clientId);
			return user == null ? false : true;
		}

		public async Task<IEnumerable<User>> GetAll()
		{
			return _context.Set<User>();
		}

		public async Task Add(User entity)
		{
			await _context.Set<User>().AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Remove(int id)
		{
			var entity = await Get(id);
			_context.Set<User>().Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Update(User entity)
		{
			_context.Set<User>().Update(entity);
			await _context.SaveChangesAsync();
		}

	}
}
