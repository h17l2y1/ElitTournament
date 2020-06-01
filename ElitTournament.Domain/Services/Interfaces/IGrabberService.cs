using ElitTournament.Domain.Views;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Services.Interfaces
{
	public interface IGrabberService
	{
		Task GrabbElitTournament();

		GrabbElitTournamentView GetElitTournament();
	}
}
