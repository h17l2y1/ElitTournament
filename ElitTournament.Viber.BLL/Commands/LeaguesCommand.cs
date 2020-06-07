using ElitTournament.Core.Entities;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Viber.BLL.Constants;
using ElitTournament.Viber.BLL.View;
using ElitTournament.Viber.Core.Enums;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;
using System.Collections.Generic;
using System.Linq;

namespace ElitTournament.Viber.BLL.Commands
{
	public class LeaguesCommand : Command
	{
		private readonly ICacheHelper _cacheHelper;
		private readonly string _firstPossibleComand;
		private readonly string _secondPossibleComand;

		public LeaguesCommand(ICacheHelper cacheHelper) : base("all leages")
		{
			_cacheHelper = cacheHelper;

			_firstPossibleComand = "начать";
			_secondPossibleComand = "назад";
		}

		public override bool Contains(string command)
		{
			var c1 = command.ToLower().Contains(Name.ToLower())
				 || command.ToLower().Contains(_firstPossibleComand)
				 || command.ToLower().Contains(_secondPossibleComand);

			var c = command.ToLower().Contains(_firstPossibleComand);

			return c;
		}

		public async override void Execute(RootObject rootObject, IViberBotClient client)
		{
			KeyboardMessage msg = SendLeagues(rootObject.Sender.Id);
			long result = await client.SendKeyboardMessageAsync(msg);
		}

		public KeyboardMessage SendLeagues(string userId)
		{
			List<League> leagues = _cacheHelper.GetLeagues();

			var keyboardMessage = new KeyboardMessage
			{
				Receiver = userId,
				Text = "Для просмотра расписание выберите лигу, а потом команду.",
				Sender = new UserBase
				{
					Name = Constant.BOT_NAME,
					Avatar = Constant.BOT_AVATAR
				},
				Keyboard = new Keyboard
				{
					DefaultHeight = true,
					Buttons = leagues.Select(p => new Core.Models.Button
					{
						Columns = 3,
						Rows = 1,
						BackgroundColor = "#E1E5E4",
						ActionType = KeyboardActionType.Reply,
						ActionBody = p.Name,
						Text = p.Name,
						TextSize = TextSize.Regular
					}).ToList()
				},
				TrackingData = "td"
			};
			return keyboardMessage;
		}

	}
}
