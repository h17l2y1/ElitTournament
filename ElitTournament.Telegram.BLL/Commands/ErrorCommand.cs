using ElitTournament.Telegram.BLL.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ElitTournament.Telegram.BLL.Commands
{
	public class ErrorCommand : Command
	{
		public ErrorCommand() : base(0)
		{
		}

		public async override Task Execute(Message message, ITelegramBotClient client)
		{       
			await client.SendTextMessageAsync(message.Chat.Id, MessageConstant.CACHE_EMPTY, ParseMode.Html, false, false, 0, GetMenu());
		}

		private ReplyKeyboardMarkup GetMenu()
		{
			IEnumerable<KeyboardButton> buttonList = new List<KeyboardButton> { new KeyboardButton(MessageConstant.REFRESH) };
			ReplyKeyboardMarkup menu = new ReplyKeyboardMarkup(buttonList, true, true);
			return menu;
		}
	}
}
