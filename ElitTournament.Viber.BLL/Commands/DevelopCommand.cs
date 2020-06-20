using ElitTournament.DAL.Repositories.Interfaces;
using ElitTournament.Viber.BLL.Constants;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Commands
{
	public class DevelopCommand : Command
	{
		private readonly ILeagueRepository _leagueRepository;

		public DevelopCommand(ILeagueRepository leagueRepository, int lastVersion) : base(lastVersion)
		{
			_leagueRepository = leagueRepository;
		}

		public async override Task<bool> Contains(string command)
		{
			return command.Contains(ButtonConstant.DEVELOP);
		}

		public async override Task Execute(Callback callback, IViberBotClient client)
		{
			TextMessage msg = SendShedule(callback);
			long result = await client.SendTextMessageAsync(msg);

			LeaguesCommand leaguesCommand = new LeaguesCommand(_leagueRepository, version);
			await leaguesCommand.Execute(callback, client);
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
