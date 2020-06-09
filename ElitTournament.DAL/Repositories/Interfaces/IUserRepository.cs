using ElitTournament.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.DAL.Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task<User> Get(int id);

		Task<IEnumerable<User>> GetAll();

		Task Add(User entity);

		Task Remove(int id);

		Task<bool> IsExist(string clientId);
	}
}
