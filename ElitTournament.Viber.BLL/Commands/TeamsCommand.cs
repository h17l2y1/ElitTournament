﻿using ElitTournament.Core.Entities;
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
	public class TeamsCommand : Command
	{
		private readonly ICacheHelper _cacheHelper;

		public TeamsCommand(ICacheHelper cacheHelper)
		{
			_cacheHelper = cacheHelper;
		}

		public override bool Contains(string command)
		{
			ICollection<string> leages = _cacheHelper.GetLeagues().Select(p => p.Name).ToList();
			return leages.Contains(command);
		}

		public async override void Execute(Callback callback, IViberBotClient client)
		{
			KeyboardMessage msg = GetTeams(callback);
			long result = await client.SendKeyboardMessageAsync(msg);
		}

		public KeyboardMessage GetTeams(Callback callback)
		{
			List<League> leagues = _cacheHelper.GetLeagues();

			League league = leagues.SingleOrDefault(x => x.Name == callback.Message.Text);

			var keyboardMessage = new KeyboardMessage
			{
				Receiver = callback.Sender.Id,
				Text = MessageConstant.CHOOSE_TEAM,
				Sender = new UserBase
				{
					Name = MessageConstant.BOT_NAME,
					Avatar = MessageConstant.BOT_AVATAR,
				},
				Keyboard = new Keyboard
				{
					DefaultHeight = true,
					Buttons = league.Teams.Select(p => new Button
					{
						Columns = 3,
						Rows = 1,
						BackgroundColor = ButtonConstant.DEFAULT_COLOR,
						ActionType = KeyboardActionType.Reply,
						ActionBody = p,
						Text = p,
						TextSize = TextSize.Regular
					}).ToList()
				},
			};

			keyboardMessage.Keyboard.Buttons.Add(new Button()
			{
				BackgroundColor = ButtonConstant.RED_COLOR,
				ActionType = KeyboardActionType.Reply,
				ActionBody = ButtonConstant.BACK,
				Text = MessageConstant.BACK,
				TextSize = TextSize.Regular
			});

			return keyboardMessage;
		}
	}
}
