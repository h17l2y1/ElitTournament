using ElitTournament.DAL.Entities;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Services.Interfaces
{
	public interface IViberBotService
	{
		Task<SetWebhookResponse> SetWebHookAsync();

		Task<IAccountInfo> GetAccountInfo();

		Task Update(Callback view);

		Task<IEnumerable<User>> GetAllUsers();
	}
}
