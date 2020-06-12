using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ElitTournament.Telegram.BLL.Services.Interfaces
{
	public interface ITelegramBotService
	{
		Task SetWebhookAsync();

		Task<IEnumerable<DAL.Entities.User>> GetAllUsers();

		Task Update(Update update);
	}
}
