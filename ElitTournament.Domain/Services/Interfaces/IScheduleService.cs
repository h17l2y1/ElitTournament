using System.Threading.Tasks;

namespace ElitTournament.Domain.Services.Interfaces
{
	public interface IScheduleService
	{
		string FindGame(string teamName);
	}
}
