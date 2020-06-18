using ElitTournament.Viber.BLL.Constants;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;
using System.Linq;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Commands
{
	public class ErrorCommand : Command
	{
		public ErrorCommand() : base(0)
		{
		}
		
		public async override Task Execute(Callback callback, IViberBotClient client)
		{
			KeyboardMessage msg = GetErrorMessage(callback);
			long result = await client.SendKeyboardMessageAsync(msg);
		}

		public KeyboardMessage GetErrorMessage(Callback callback)
		{
			var keyboardMessage = new KeyboardMessage(callback.Sender.Id, MessageConstant.CACHE_EMPTY)
			{
				Sender = new UserBase
				{
					Name = MessageConstant.BOT_NAME,
					Avatar = MessageConstant.BOT_AVATAR,
				},
				Keyboard = new Keyboard
				{
					DefaultHeight = true,
					Buttons = Enumerable.Range(0, 1)
										.Select(x => new Button(ButtonConstant.REFRESH, MessageConstant.REFRESH))
										.ToList()
				},
			};

			return keyboardMessage;
		}
	}
}
