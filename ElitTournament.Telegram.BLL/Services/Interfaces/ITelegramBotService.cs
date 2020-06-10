using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ElitTournament.Telegram.BLL.Services.Interfaces
{
	public interface ITelegramBotService
	{
		Task SetWebhookAsync();

		Task<IEnumerable<Core.Entities.User>> GetAllUsers();

		Task Update(Update update);
	}
}
