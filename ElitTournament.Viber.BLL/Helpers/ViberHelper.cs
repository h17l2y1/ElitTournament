using ElitTournament.Core.Entities;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Viber.BLL.Helpers.Interfaces;
using ElitTournament.Viber.BLL.View;
using ElitTournament.Viber.Core.Enums;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Helpers
{
	public class ViberHelper : IViberHelper
	{
		private readonly IViberBotClient _viberBotClient;
		private readonly ICacheHelper _cacheHelper;
		private readonly string _botName;
		private readonly string _botAvatar;

		public ViberHelper(IConfiguration configuration, IViberBotClient viberBotClient, ICacheHelper cacheHelper)
		{
			_viberBotClient = viberBotClient;
			_cacheHelper = cacheHelper;
			_botName = configuration.GetSection($"Bot:Name").Value;
			_botAvatar = configuration.GetSection($"Bot:Avatar").Value;
		}

		public async Task SendTextMessage(RootObject view)
		{
			await SendLeagues(view);

			//var shedule = _cacheHelper.FindGame(view.Message.Text);

			//long result = await _viberBotClient.SendTextMessageAsync(new TextMessage
			//{
			//	Receiver = view.Sender.Id,
			//	Sender = new UserBase
			//	{
			//		Name = _botName,
			//		Avatar = _botAvatar
			//	},
			//	Text = shedule,
			//	MinApiVersion = 1,
			//	TrackingData = "tracking data"
			//});
			return;
		}

		// ligi
		public async Task SendLeagues(RootObject view)
		{
			List<League> leagues = _cacheHelper.GetLeagues();

			var test = new KeyboardMessage
			{
				Receiver = view.Sender.Id,
				Text = "Для просмотра расписание выберите лигу, а потом команду.",
				Sender = new UserBase
				{
					Name = _botName,
					Avatar = _botAvatar
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

			var result = await _viberBotClient.SendKeyboardMessageAsync(test);
		}

		// comandy
		public async Task SendTeams(RootObject view)
		{
			List<League> leagues = _cacheHelper.GetLeagues();

			var league = leagues.SingleOrDefault(x => x.Name == view.Message.Text);


			var test = new KeyboardMessage
			{
				Receiver = view.Sender.Id,
				Text = "Для просмотра расписание выберите лигу, а потом команду.",
				Sender = new UserBase
				{
					Name = _botName,
					Avatar = _botAvatar
				},
				Keyboard = new Keyboard
				{
					DefaultHeight = true,
					Buttons = league.Teams.Select(p => new Core.Models.Button
					{
						Columns = 3,
						Rows = 1,
						BackgroundColor = "E1E5E4",
						ActionType = KeyboardActionType.Reply,
						ActionBody = p,
						Text = p,
						TextSize = TextSize.Regular
					}).ToList()
				},
				TrackingData = "td"
			};

			test.Keyboard.Buttons.Add(new Core.Models.Button()
			{
				BackgroundColor = "#E1E5E4",
				ActionType = KeyboardActionType.Reply,
				ActionBody = "Назад",
				Text = "Назад",
				TextSize = TextSize.Regular
			});

			var result = await _viberBotClient.SendKeyboardMessageAsync(test);
		}

		// find game
		public async Task SendShedule(RootObject view)
		{
			var shedule = _cacheHelper.FindGame(view.Message.Text);

			long result = await _viberBotClient.SendTextMessageAsync(new TextMessage
			{
				Receiver = view.Sender.Id,
				Sender = new UserBase
				{
					Name = _botName,
					Avatar = _botAvatar
				},
				Text = shedule,
				MinApiVersion = 1,
				TrackingData = "tracking data"
			});
		}
	}
}
