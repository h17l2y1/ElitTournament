using Telegram.Bot.Types;

namespace ElitTournament.Domain.Services.Interfaces
{
	public interface ITelegramService
	{
		void Update(Update update);
	}
}
