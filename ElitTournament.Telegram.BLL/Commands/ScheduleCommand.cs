using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Telegram.BLL.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Telegram.BLL.Commands
{
	public class ScheduleCommand : Command
	{
		private ICacheHelper _cacheHelper;

		public ScheduleCommand(ICacheHelper cacheHelper)
		{
			_cacheHelper = cacheHelper;
		}

		public override bool Contains(string text)
		{
			if (text.Contains(ButtonConstant.BACK) || text.Contains(ButtonConstant.REFRESH) ||
				text.Contains(ButtonConstant.DEVELOP) || text.Contains(ButtonConstant.START) || text.Contains(MessageConstant.START))
			{
				return false;
			}

			return true;
		}

		public async override void Execute(Message message, ITelegramBotClient client)
		{
			string shedule = _cacheHelper.FindGame(message.Text) ?? $"Игры команды \"{message.Text}\" не найдено";
			await client.SendTextMessageAsync(message.Chat.Id, shedule);
		}
	}
}
