﻿using ElitTournament.DAL.Entities;
using ElitTournament.Viber.BLL.Constants;
using ElitTournament.Viber.Core.Enums;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElitTournament.DAL.Repositories.Interfaces;

namespace ElitTournament.Viber.BLL.Commands
{
	public class TeamsCommand : Command
	{
		private readonly ILeagueRepository _leagueRepository;

		public TeamsCommand(ILeagueRepository leagueRepository, int lastVersion) : base(lastVersion)
		{
			_leagueRepository = leagueRepository;
		}

		public async override Task<bool> Contains(string command)
		{
			IEnumerable<League> teams = await _leagueRepository.GetAll(version);
			IEnumerable<string> teamNames = teams.Select(p => p.Name);
			return teamNames.Contains(command);
		}

		public async override Task Execute(Callback callback, IViberBotClient client)
		{
			KeyboardMessage msg = await GetTeams(callback);
			long result = await client.SendKeyboardMessageAsync(msg);
		}

		public async Task<KeyboardMessage> GetTeams(Callback callback)
		{
			League league;
			
			IEnumerable<League> leagues = await _leagueRepository.GetAll(version);

			league = leagues.SingleOrDefault(x => x.Name == callback.Message.TrackingData);
			
			if (league == null)
			{
				league = leagues.SingleOrDefault(x => x.Name == callback.Message.Text);
			}
			
			var sortedTeams= league?.Teams.OrderByDescending(o => o.Position);
			
			var keyboardMessage = new KeyboardMessage(callback.Sender.Id, MessageConstant.CHOOSE_TEAM)
			{
				Sender = new UserBase
				{
					Name = MessageConstant.BOT_NAME,
					Avatar = MessageConstant.BOT_AVATAR,
				},
				Keyboard = new Keyboard
				{
					DefaultHeight = true,
					Buttons = CreateButtons(sortedTeams)
				},
				TrackingData = callback.Message.Text
			};
			
			return keyboardMessage;
		}
		
		private ICollection<Button> CreateButtons(IEnumerable<Team> sortedTeams)
		{
			List<Button> buttons = new List<Button>();
			
			// buttons.Add(new Button()
			// {
			// 	Columns = 6,
			// 	Rows = 1,
			// 	BackgroundColor = ButtonConstant.DEFAULT_COLOR,
			// 	ActionBody = ButtonConstant.TABLE,
			// 	Text = MessageConstant.TABLE,
			// });

			buttons.AddRange(sortedTeams.Select(p => new Button(p.Name, p.Name)
			{
				Columns = 3,
				Rows = 1,
				BackgroundColor = ButtonConstant.DEFAULT_COLOR,
			}));
			
			buttons.Add(new Button()
			{
				Columns = 6,
				Rows = 1,
				BackgroundColor = ButtonConstant.RED_COLOR,
				ActionBody = ButtonConstant.BACK,
				Text = MessageConstant.BACK,
			});

			return buttons;
		}
		
	}
}
