using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Viber.BLL.Constants;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;

namespace ElitTournament.Viber.BLL.Commands
{
	public class DevelopCommand : Command
	{
		private readonly ICacheHelper _cacheHelper;

		public DevelopCommand(ICacheHelper cacheHelper)
		{
			_cacheHelper = cacheHelper;
		}

		public override bool Contains(string command)
		{
			return command.Contains(ButtonConstant.DEVELOP);
		}

		public async override void Execute(Callback callback, IViberBotClient client)
		{
			TextMessage msg = SendShedule(callback);
			long result = await client.SendTextMessageAsync(msg);

			LeaguesCommand leaguesCommand = new LeaguesCommand(_cacheHelper);
			leaguesCommand.Execute(callback, client);
		}

		public TextMessage SendShedule(Callback callback)
		{
			string id = callback.Sender?.Id ?? callback.User?.Id;
			string text = "В разработке: \n1.Турнирные таблицы \n2. Бот будет сам уведомлять когда игра\n3. ...\n\nСвязь с разработчиком\n0955923228";

			var textMessage = new TextMessage(id, text)
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
