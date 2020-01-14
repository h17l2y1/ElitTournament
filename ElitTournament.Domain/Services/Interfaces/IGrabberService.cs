using ElitTournament.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Services.Interfaces
{
	public interface IGrabberService
	{
		Task<string> GrabbSchedule();

		Task<string> UpdateSchedule();

		Task<List<League>> GrabbLeagues();
	}
}
