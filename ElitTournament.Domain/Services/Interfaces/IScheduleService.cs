using ElitTournament.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Services.Interfaces
{
	public interface IScheduleService
	{
		string FindGame(string teamName);

		Task<List<League>> GetLeagues();
	}
}
