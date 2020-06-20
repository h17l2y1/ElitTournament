using System;
using System.Threading.Tasks;
using ElitTournament.DAL.Repositories.Interfaces;
using ElitTournament.Telegram.BLL.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace ElitTournament.Telegram.BLL.Commands
{
	public class TableCommand : Command
	{
		private readonly ILeagueRepository _leagueRepository;
		
		public TableCommand(ILeagueRepository leagueRepository, int lastVersion) : base(lastVersion)
		{
			_leagueRepository = leagueRepository;
		}

		public async override Task<bool> Contains(string command)
		{
			return command.Contains(ButtonConstant.TABLE);
		}
		
		public override async Task Execute(Message message, ITelegramBotClient client)
		{
			string url = await GetImage(message);
			var image = new InputOnlineFile(new Uri(url));
			await client.SendPhotoAsync(message.Chat.Id, image);
		}

		public async Task<string> GetImage(Message message)
		{
			string leagueName = message.Text.Remove(0, 17);
			string link = await  _leagueRepository.GetTableLink(leagueName);
			return link;
		}
	}
}