﻿using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Viber.BLL.Constants;
using ElitTournament.Viber.Core.Enums;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;
using System.Linq;

namespace ElitTournament.Viber.BLL.Commands
{
	public class WelcomCommand : Command
	{
		private readonly ICacheHelper _cacheHelper;

		public WelcomCommand(ICacheHelper cacheHelper)
		{
			_cacheHelper = cacheHelper;
		}

		public override bool Contains(string text)
		{
			return text == null ? true : false;
		}

		public async override void Execute(Callback callback, IViberBotClient client)
		{
			KeyboardMessage msg = GetWelcomeMessage(callback);
			long result = await client.SendKeyboardMessageAsync(msg);
		}

		public KeyboardMessage GetWelcomeMessage(Callback callback)
		{
			var keyboardMessage = new KeyboardMessage
			{
				Receiver = callback.User.Id,
				Text = MessageConstant.WELCOME_MESSAGE,
				Sender = new UserBase
				{
					Name = MessageConstant.BOT_NAME,
					Avatar = MessageConstant.BOT_AVATAR,
				},
				Keyboard = new Keyboard
				{
					DefaultHeight = true,
					Buttons = Enumerable.Range(0, 1).Select(x =>
					 new Button
					 {
						 BackgroundColor = ButtonConstant.DEFAULT_COLOR,
						 ActionType = KeyboardActionType.Reply,
						 ActionBody = ButtonConstant.START,
						 Text = MessageConstant.START,
						 TextSize = TextSize.Regular
					 }).ToList()
				},
			};

			return keyboardMessage;
		}
	}
}
