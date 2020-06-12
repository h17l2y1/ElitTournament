using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Viber.BLL.Constants;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Commands
{
	public class ScheduleCommand : Command
	{
		private readonly ICacheHelper _cacheHelper;

		public ScheduleCommand(ICacheHelper cacheHelper)
		{
			_cacheHelper = cacheHelper;
		}

		public override bool Contains(string text)
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
			TextMessage msg = GetSchedule(callback);
			long result = await client.SendTextMessageAsync(msg);

			LeaguesCommand leaguesCommand = new LeaguesCommand(_cacheHelper);
			await leaguesCommand.Execute(callback, client);
		}

		public TextMessage GetSchedule(Callback callback)
		{
			string shedule = _cacheHelper.FindGame(callback.Message.Text) ?? $"Игры команды \"{callback.Message.Text}\" не найдено";
		
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
