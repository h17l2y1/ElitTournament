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
	public class TeamsCommand : Command
	{
		private readonly ICacheHelper _cacheHelper;

		public TeamsCommand(ICacheHelper cacheHelper) : base("all leages")
		{
			_cacheHelper = cacheHelper;
		}

		public async override void Execute(RootObject rootObject, IViberBotClient client)
		{
			KeyboardMessage msg = SendTeams(rootObject);
			long result = await client.SendKeyboardMessageAsync(msg);
		}

		public override bool Contains(string command)
		{
			ICollection<string> leages = _cacheHelper.GetLeagues().Select(p => p.Name).ToList();
			return leages.Contains(command);
		}

		public KeyboardMessage SendTeams(RootObject rootObject)
		{
			List<League> leagues = _cacheHelper.GetLeagues();

			League league = leagues.SingleOrDefault(x => x.Name == rootObject.Message.Text);

			var keyboardMessage = new KeyboardMessage
			{
				Receiver = rootObject.Sender.Id,
				Text = "Для просмотра расписание выберите лигу, а потом команду.",
				Sender = new UserBase 
				{
					Name = Constant.BOT_NAME,
					Avatar = Constant.BOT_AVATAR,
				},
				Keyboard = new Keyboard
				{
					DefaultHeight = true,
					Buttons = league.Teams.Select(p => new Core.Models.Button
					{
						Columns = 3,
						Rows = 1,
						BackgroundColor = "#E1E5E4",
						ActionType = KeyboardActionType.Reply,
						ActionBody = p,
						Text = p,
						TextSize = TextSize.Regular
					}).ToList()
				},
				TrackingData = "td"
			};

			keyboardMessage.Keyboard.Buttons.Add(new Core.Models.Button()
			{
				BackgroundColor = "#E1E5E4",
				ActionType = KeyboardActionType.Reply,
				ActionBody = "Назад",
				Text = "Назад",
				TextSize = TextSize.Regular
			});

			return keyboardMessage;

		}
	}
}
