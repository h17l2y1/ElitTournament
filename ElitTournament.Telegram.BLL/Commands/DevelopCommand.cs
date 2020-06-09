using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Telegram.BLL.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Telegram.BLL.Commands
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
			return command.Contains(MessageConstant.DEVELOP);
		}

		public override void Execute(Message message, ITelegramBotClient client)
		{
			var c = new LeagueCommand(_cacheHelper);
			c.Execute(message, client);
		}
	}
}
