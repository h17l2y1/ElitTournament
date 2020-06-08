using ElitTournament.Viber.BLL.Constants;
using ElitTournament.Viber.Core.Enums;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;
using System.Linq;

namespace ElitTournament.Viber.BLL.Commands
{
	public class ErrorCommand : Command
	{
		public override bool Contains(string text)
		{
			return true;
		}

		public async override void Execute(Callback callback, IViberBotClient client)
		{
			KeyboardMessage msg = GetErrorMessage(callback);
			long result = await client.SendKeyboardMessageAsync(msg);
		}

		public KeyboardMessage GetErrorMessage(Callback callback)
		{
			var keyboardMessage = new KeyboardMessage
			{
				Receiver = callback.Sender.Id,
				Text = MessageConstant.CACHE_EMPTY,
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
						 ActionBody = ButtonConstant.REFRESH,
						 Text = MessageConstant.REFRESH,
						 TextSize = TextSize.Regular
					 }).ToList()
				},
			};

			return keyboardMessage;
		}
	}
}
