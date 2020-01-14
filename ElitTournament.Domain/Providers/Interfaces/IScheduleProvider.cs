using ElitTournament.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Providers.Interfaces
{
	public interface IGrabbScheduleProvider
	{
		Task<List<Schedule>> GetSchedule();
	}
}
