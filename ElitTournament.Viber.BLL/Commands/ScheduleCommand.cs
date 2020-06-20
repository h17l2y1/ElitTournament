using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.DAL.Repositories.Interfaces;
using ElitTournament.Viber.BLL.Constants;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Commands
{
	public class ScheduleCommand : Command
	{
		private readonly IScheduleRepository _scheduleRepository;
		private readonly ILeagueRepository _leagueRepository;

		public ScheduleCommand(IScheduleRepository scheduleRepository, ILeagueRepository leagueRepository, int lastVersion) : base(lastVersion)
		{
			_scheduleRepository = scheduleRepository;
			_leagueRepository = leagueRepository;
		}

		public async override Task<bool> Contains(string text)
		{
			if (text.Contains(ButtonConstant.BACK) || text.Contains(ButtonConstant.REFRESH) ||
				text.Contains(ButtonConstant.DEVELOP) || text.Contains(ButtonConstant.START))
			{
				return false;
			}

			return true;
		}

		public async override Task Execute(Callback callback, IViberBotClient client)
		{
			TextMessage msg = await GetSchedule(callback);
			long result = await client.SendTextMessageAsync(msg);

			LeaguesCommand leaguesCommand = new LeaguesCommand(_leagueRepository, version);
			await leaguesCommand.Execute(callback, client);
		}

		public async Task<TextMessage> GetSchedule(Callback callback)
		{
			string shedule = await _scheduleRepository.FindGame(callback.Message.Text, version) ?? $"Игры команды \"{callback.Message.Text}\" не найдено";
		
			var textMessage = new TextMessage(callback.Sender.Id, shedule)
			{
				Sender = new UserBase
				{
					Name = MessageConstant.BOT_NAME,
					Avatar = MessageConstant.BOT_AVATAR
				},
			};

			return textMessage;
		}

	}
}
