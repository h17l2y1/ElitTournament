using ElitTournament.Core.Views;
using System.Threading.Tasks;

namespace ElitTournament.Core.Services.Interfaces
{
	public interface IGrabberService
	{
		Task GrabbElitTournament();

		Task<GrabbElitTournamentView> GetElitTournament();

		Task<string> FindGame(string team);
	}
}
