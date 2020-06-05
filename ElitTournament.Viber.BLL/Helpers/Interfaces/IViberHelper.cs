using ElitTournament.Viber.BLL.View;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Helpers.Interfaces
{
	public interface IViberHelper
	{
		Task SendTextMessage(RootObject view);
	}
}
