using ElitTournament.DAL.Entities;
using ElitTournament.DAL.Config;
using ElitTournament.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ElitTournament.DAL.Repositories
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(ApplicationContext context) : base(context)
		{
		}

		public async Task<bool> IsExist(string clientId)
		{
			User user = await _dbSet.AsNoTracking().SingleOrDefaultAsync(p => p.ClientId == clientId);
			return user == null ? false : true;
		}
	}
}
