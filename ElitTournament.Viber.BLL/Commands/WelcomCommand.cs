using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Viber.BLL.Constants;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;
using System.Linq;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Commands
{
	public class WelcomCommand : Command
	{
		public async override Task<bool> Contains(string text)
		{
			return text == null ? true : false;
		}

		public async override Task Execute(Callback callback, IViberBotClient client)
		{
			KeyboardMessage msg = GetWelcomeMessage(callback);
			long result = await client.SendKeyboardMessageAsync(msg);
		}

		public KeyboardMessage GetWelcomeMessage(Callback callback)
		{
			var keyboardMessage = new KeyboardMessage(callback.User.Id, MessageConstant.WELCOME_MESSAGE)
			{
				Sender = new UserBase
				{
					Name = MessageConstant.BOT_NAME,
					Avatar = MessageConstant.BOT_AVATAR,
				},
				Keyboard = new Keyboard
				{
					DefaultHeight = true,
					Buttons = Enumerable.Range(0, 1).Select(x => new Button(ButtonConstant.START, MessageConstant.START)).ToList()
				},
			};

			return keyboardMessage;
		}
	}
}
