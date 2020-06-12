using ElitTournament.DAL.Entities;
using System.Threading.Tasks;

namespace ElitTournament.DAL.Repositories.Interfaces
{
	public interface IScheduleRepository : IBaseRepository<Schedule>
	{
		Task<string> FindGame(string teamName);
	}
}
