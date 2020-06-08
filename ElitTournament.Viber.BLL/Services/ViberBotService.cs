﻿using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Viber.BLL.Commands;
using ElitTournament.Viber.BLL.Helpers.Interfaces;
using ElitTournament.Viber.BLL.Services.Interfaces;
using ElitTournament.Viber.BLL.View;
using ElitTournament.Viber.Core.Enums;
using ElitTournament.Viber.Core.Model;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;
using ElitTournament.Viber.Core.View;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Services
{
	public class ViberBotService : IViberBotService
	{
		private readonly IViberBotClient _viberBotClient;
		private readonly ICacheHelper _cacheHelper;
		private readonly string _webHook;
		private readonly string _viberUrl;
		private List<Command> _commands;
		private readonly string _botName;
		private readonly string _botAvatar;

		public ViberBotService(IConfiguration configuration, IViberBotClient viberBotClient, ICacheHelper cacheHelper)
		{
			_viberBotClient = viberBotClient;
			_cacheHelper = cacheHelper;
			_webHook = configuration["WebHook"];
			_viberUrl = "/api/viber/update";
			_botName = configuration.GetSection($"Bot:Name").Value;
			_botAvatar = configuration.GetSection($"Bot:Avatar").Value;
		}

		public async Task<SetWebhookResponse> SetWebHookAsync()
		{
			string webHook = $"{_webHook}{_viberUrl}";
			SetWebhookResponse res = await _viberBotClient.SetWebhookAsync(webHook);
			return res;
		}

		public async Task<IAccountInfo> GetAccountInfo()
		{
			IAccountInfo res = await _viberBotClient.GetAccountInfoAsync();
			return res;
		}

		public async Task<long> SendTextMessage(string text)
		{
			TextMessage message = new TextMessage { Text = text };

			long res = await _viberBotClient.SendTextMessageAsync(message);
			return res;
		}

		public void Update(Callback callback)
		{
			if (callback.Event == EventType.Webhook)
			{
				return;
			}

			if (callback.Event == EventType.Message)
			{
				SendMessage(callback);
				return;
			}
		}

		private void InitCommands()
		{
			_commands = new List<Command>
			{
				new LeaguesCommand(_cacheHelper),
				new TeamsCommand(_cacheHelper),
				new ScheduleCommand(_cacheHelper)
			};
		}

		public void SendMessage(Callback callback)
		{
			InitCommands();

			foreach (Command command in _commands)
			{
				bool isTeamExist = command.Contains(callback?.Message?.Text);
				if (isTeamExist)
				{
					command.Execute(callback, _viberBotClient);
					break;
				}
			}
		}

	}

}
