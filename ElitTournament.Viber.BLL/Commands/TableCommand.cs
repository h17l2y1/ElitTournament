using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.DAL.Entities;
using ElitTournament.DAL.Repositories.Interfaces;
using ElitTournament.Viber.BLL.Constants;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;

namespace ElitTournament.Viber.BLL.Commands
{
	public class TableCommand : Command
	{
		private readonly IImageHelper _imageHelper;
		private readonly ILeagueRepository _leagueRepository;
		
		public TableCommand(ILeagueRepository leagueRepository, IImageHelper imageHelper, int lastVersion) : base(lastVersion)
		{
			_imageHelper = imageHelper;
			_leagueRepository = leagueRepository;
		}

		public async override Task<bool> Contains(string command)
		{
			return command.Contains(ButtonConstant.TABLE);
		}

		public async override Task Execute(Callback callback, IViberBotClient client)
		{
			PictureMessage msg = await GetImage(callback);
			long result = await client.SendPictureMessageAsync(msg);

			TeamsCommand teamsCommand = new TeamsCommand(_leagueRepository, version);
			await teamsCommand.Execute(callback, client);
		}
		
		public async Task<PictureMessage> GetImage(Callback callback)
		{
			string link = await  _leagueRepository.GetTableLink(callback.Message.TrackingData);
		
			var pictureMessage = new PictureMessage(callback.Sender.Id)
			{
				Sender = new UserBase
				{
					Name = MessageConstant.BOT_NAME,
					Avatar = MessageConstant.BOT_AVATAR,
				},
				Text = $"{MessageConstant.TABLE} {callback.Message.TrackingData}",
				Media = link,
				Thumbnail = link,
			};

			return pictureMessage;
		}
	}
}