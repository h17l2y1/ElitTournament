using System.Threading.Tasks;
using ElitTournament.DAL.Repositories.Interfaces;
using ElitTournament.Telegram.BLL.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Telegram.BLL.Commands
{
	public class DevelopCommand : Command
	{
		private readonly ILeagueRepository _leagueRepository;

		public DevelopCommand(ILeagueRepository leagueRepository, int lastVersion) : base(lastVersion)
		{
			_leagueRepository = leagueRepository;
		}

		public override async Task<bool> Contains(string command)
		{
			return command.Contains(MessageConstant.DEVELOP);
		}

		public async override Task Execute(Message message, ITelegramBotClient client)
		{
			LeagueCommand leaguesCommand = new LeagueCommand(_leagueRepository, version);
			await leaguesCommand.Execute(message, client);
		}
	}
}
