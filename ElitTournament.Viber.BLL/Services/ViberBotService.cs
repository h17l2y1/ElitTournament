using ElitTournament.Core.Entities;
using ElitTournament.Core.Helpers;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.DAL.Repositories.Interfaces;
using ElitTournament.Viber.BLL.Commands;
using ElitTournament.Viber.BLL.Services.Interfaces;
using ElitTournament.Viber.Core.Enums;
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
		private readonly IUserRepository _userRepository;
		private readonly IViberBotClient _viberBotClient;
		private readonly ICacheHelper _cacheHelper;
		private readonly string _webHook;
		private readonly string _viberUrl;
		private List<Command> _commands;

		public ViberBotService(IConfiguration configuration, IViberBotClient viberBotClient, ICacheHelper cacheHelper, IUserRepository userRepository)
		{
			_userRepository = userRepository;
			_viberBotClient = viberBotClient;
			_cacheHelper = cacheHelper;
			_webHook = configuration["WebHook"];
			_viberUrl = "/api/viber/update";
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

		public async Task Update(Callback callback)
		{
			if (callback.Event == EventType.Webhook)
			{
				return;
			}

			if (callback.Event == EventType.ConversationStarted)
			{
				ConversationStarted(callback);
				return;
			}

			if (callback.Event == EventType.Message)
			{
				await GetUserDetails(callback);
				await SendMessage(callback);
				return;
			}
		}

		private void InitCommands()
		{
			_commands = new List<Command>
			{
				new ErrorCommand(),
				new DevelopCommand(_cacheHelper),
				new TeamsCommand(_cacheHelper),
				new ScheduleCommand(_cacheHelper),
				new LeaguesCommand(_cacheHelper),
			};

			if (_cacheHelper.IsCacheExist)
			{
				_commands.RemoveAt(0);
			}
		}

		private async Task SendMessage(Callback callback)
		{
			//await GetUserDetails(callback);
			InitCommands();

			foreach (Command command in _commands)
			{
				bool isTeamExist = command.Contains(callback?.Message?.Text.Trim());
				if (isTeamExist)
				{
					command.Execute(callback, _viberBotClient);
					break;
				}
			}
		}

		private void ConversationStarted(Callback callback)
		{
			var command = new WelcomCommand(_cacheHelper);
			command.Execute(callback, _viberBotClient);
		}


		//	need to implement this
		private async Task GetUserDetails(Callback callback)
		{
			bool userIsExist = await _userRepository.IsExist(callback.Sender.Id);
			if (!userIsExist)
			{
				UserDetails viberUser = await _viberBotClient.GetUserDetailsAsync(callback.Sender.Id);

				User newUser = new User();
				await _userRepository.Add(newUser);
			}
		}

	}

}
