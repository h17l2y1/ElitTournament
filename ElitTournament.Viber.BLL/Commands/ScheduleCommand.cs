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
	public class ScheduleCommand : Command
	{
		private readonly ICacheHelper _cacheHelper;

		public ScheduleCommand(ICacheHelper cacheHelper) : base("all leages")
		{
			_cacheHelper = cacheHelper;
		}

		public async override void Execute(RootObject rootObject, IViberBotClient client)
		{
			TextMessage msg = SendShedule(rootObject);
			long result = await client.SendTextMessageAsync(msg);
			KeyboardMessage msg1 = SendLeagues(rootObject.Sender.Id);
			long result1 = await client.SendKeyboardMessageAsync(msg1);
		}

		public override bool Contains(string teamName)
		{
			// fix
			return true;
		}

		public TextMessage SendShedule(RootObject rootObject)
		{
			string shedule = _cacheHelper.FindGame(rootObject.Message.Text);

			var result = new TextMessage
			{
				Receiver = rootObject.Sender.Id,
				Sender = new UserBase
				{
					Name = Constant.BOT_NAME,
					Avatar = Constant.BOT_AVATAR
				},
				Text = shedule,
				MinApiVersion = 1,
				TrackingData = "tracking data"
			};

			return result;
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
