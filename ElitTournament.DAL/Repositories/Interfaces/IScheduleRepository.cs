using ElitTournament.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.DAL.Repositories.Interfaces
{
	public interface IScheduleRepository : IBaseRepository<Schedule>
	{
		Task<IEnumerable<Schedule>> GetAll(int version);

		Task<string> FindGame(string teamName, int version);
	}
}
