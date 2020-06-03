using ElitTournament.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Core.Services.Interfaces
{
	public interface IScheduleService
	{
		string FindGame(string teamName);

		Task<List<League>> GetLeagues();
	}
}
