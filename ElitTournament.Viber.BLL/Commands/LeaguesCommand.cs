using ElitTournament.DAL.Entities;
using ElitTournament.DAL.Repositories.Interfaces;
using ElitTournament.Viber.BLL.Constants;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Commands
{
	public class LeaguesCommand : Command
	{
		private readonly ILeagueRepository _leagueRepository;

		public LeaguesCommand(ILeagueRepository leagueRepository, int lastVersion) : base(lastVersion)
		{
			_leagueRepository = leagueRepository;
		}

		public async override Task Execute(Callback callback, IViberBotClient client)
		{
			KeyboardMessage msg = await GetLeagues(callback);
			long result = await client.SendKeyboardMessageAsync(msg);
		}

		private async Task<KeyboardMessage> GetLeagues(Callback callback)
		{
			IEnumerable<League> leagues = await _leagueRepository.GetAll(version);
			
			var keyboardMessage = new KeyboardMessage(callback.Sender.Id, MessageConstant.CHOOSE_LEAGUE)
			{
				Sender = new UserBase
				{
					Name = MessageConstant.BOT_NAME,
					Avatar = MessageConstant.BOT_AVATAR
				},
				Keyboard = new Keyboard
				{
					DefaultHeight = true,
					Buttons = CreateButtons(leagues)
				}
			};
			
			return keyboardMessage;
		}

		private ICollection<Button> CreateButtons(IEnumerable<League> leagues)
		{
			List<Button> buttons = new List<Button>();
			
			buttons.AddRange(leagues.Select(p => new Button(p.Name, p.Name)
			{
				BackgroundColor = ButtonConstant.DEFAULT_COLOR,
				Columns = 3,
				Rows = 1,
			}));
			
			buttons.Add(new Button()
			{
				Columns = 6,
				Rows = 1,
				BackgroundColor = ButtonConstant.DEFAULT_COLOR,
				ActionBody = ButtonConstant.DEVELOP,
				Text = MessageConstant.DEVELOP,
			});

			return buttons;
		}

	}
}
