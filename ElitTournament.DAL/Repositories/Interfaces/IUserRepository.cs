using ElitTournament.DAL.Entities;
using System.Threading.Tasks;

namespace ElitTournament.DAL.Repositories.Interfaces
{
	public interface IUserRepository : IBaseRepository<User>
	{
		Task<bool> IsExist(string clientId);
	}
}
