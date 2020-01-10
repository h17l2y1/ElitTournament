using ElitTournament.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Providers.Interfaces
{
	public interface IScheduleProvider
	{
		Task<List<Schedule>> GetSchedule();
	}
}
