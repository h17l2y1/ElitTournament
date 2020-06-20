using System.Threading.Tasks;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.DAL.Repositories.Interfaces;
using ElitTournament.Telegram.BLL.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Telegram.BLL.Commands
{
	public class ScheduleCommand : Command
	{
		private readonly IScheduleRepository _scheduleRepository;

		public ScheduleCommand(IScheduleRepository scheduleRepository, int lastVersion) : base(lastVersion)
		{
			_scheduleRepository = scheduleRepository;
		}

		public override async Task<bool> Contains(string text)
		{
			if (text.Contains(ButtonConstant.BACK) || text.Contains(ButtonConstant.REFRESH) ||
				text.Contains(ButtonConstant.DEVELOP) || text.Contains(ButtonConstant.START) || text.Contains(MessageConstant.START))
			{
				return false;
			}

			return true;
		}

		public override async Task Execute(Message message, ITelegramBotClient client)
		{
			string shedule = await _scheduleRepository.FindGame(message.Text) ?? $"Игры команды \"{message.Text}\" не найдено";
			await client.SendTextMessageAsync(message.Chat.Id, shedule);
		}
	}
}
