using ElitTournament.Telegram.BLL.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ElitTournament.Telegram.BLL.Commands
{
	public class WelcomCommand : Command
	{
		public WelcomCommand(int lastVersion) : base(lastVersion)
		{
		}
		public override async Task<bool> Contains(string command)
		{
			return command.ToLower().Contains(ButtonConstant.START);
		}

		public async override Task Execute(Message message, ITelegramBotClient client)
		{
			await client.SendTextMessageAsync(message.Chat.Id, MessageConstant.WELCOME_MESSAGE, ParseMode.Html, false, false, 0, GetMenu());
		}

		private ReplyKeyboardMarkup GetMenu()
		{
			IEnumerable<KeyboardButton> keyBoard = new List<KeyboardButton> { new KeyboardButton(MessageConstant.START) };
			ReplyKeyboardMarkup menu = new ReplyKeyboardMarkup(keyBoard, true, true);
			return menu;
		}


	}
}
