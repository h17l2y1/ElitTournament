using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ElitTournament.Telegram.BLL.Services.Interfaces
{
	public interface ITelegramBotService
	{
		Task SetWebhookAsync();

		void Update(Update update);
	}
}
