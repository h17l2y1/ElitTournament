using ElitTournament.Core.Entities;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Viber.BLL.Constants;
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

		public LeaguesCommand(ICacheHelper cacheHelper)
		{
			_cacheHelper = cacheHelper;
		}

		public override bool Contains(string command)
		{
			return true;
		}

		public async override void Execute(Callback callback, IViberBotClient client)
		{
			KeyboardMessage msg = GetLeagues(callback);
			long result = await client.SendKeyboardMessageAsync(msg);
		}

		public KeyboardMessage GetLeagues(Callback callback)
		{
			List<League> leagues = _cacheHelper.GetLeagues();

			var keyboardMessage = new KeyboardMessage
			{
				Receiver = callback.Sender.Id,
				Text = MessageConstant.CHOOSE_LEAGUE,
				Sender = new UserBase
				{
					Name = MessageConstant.BOT_NAME,
					Avatar = MessageConstant.BOT_AVATAR
				},
				Keyboard = new Keyboard
				{
					DefaultHeight = true,
					Buttons = leagues.Select(p => new Button
					{
						Columns = 3,
						Rows = 1,
						BackgroundColor = ButtonConstant.DEFAULT_COLOR,
						ActionType = KeyboardActionType.Reply,
						ActionBody = p.Name,
						Text = p.Name,
						TextSize = TextSize.Regular
					}).ToList()
				}
			};

			keyboardMessage.Keyboard.Buttons.Add(new Button()
			{
				BackgroundColor = ButtonConstant.DEFAULT_COLOR,
				ActionType = KeyboardActionType.Reply,
				ActionBody = ButtonConstant.DEVELOP,
				Text = MessageConstant.DEVELOP,
				TextSize = TextSize.Regular
			});

			return keyboardMessage;
		}

	}
}
