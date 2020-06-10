using AutoMapper;
using ElitTournament.Core.Entities;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.DAL.Repositories.Interfaces;
using ElitTournament.Viber.BLL.Commands;
using ElitTournament.Viber.BLL.Services.Interfaces;
using ElitTournament.Viber.Core.Enums;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.View;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Services
{
	public class ViberBotService : IViberBotService
	{
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IViberBotClient _viberBotClient;
		private readonly ICacheHelper _cacheHelper;
		private readonly string _webHook;
		private readonly string _viberUrl;
		private List<Command> _commands;

		public ViberBotService(IConfiguration configuration, IViberBotClient viberBotClient, ICacheHelper cacheHelper,
			IUserRepository userRepository, IMapper mapper)
		{
			_mapper = mapper;
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

		public async Task<IEnumerable<User>> GetAllUsers()
		{
			IEnumerable<User> users = await _userRepository.GetAll();
			return users;
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

		private async Task GetUserDetails(Callback callback)
		{
			bool userIsExist = await _userRepository.IsExist(callback.Sender.Id);
			if (!userIsExist)
			{
				//var jsonString = "{\"primary_device_os\":\"Android 6.0\",\"viber_version\":\"12.9.5.2\",\"mcc\":0,\"mnc\":0,\"device_type\":\"M5s\",\"id\":\"+sTRbtkHmuy5QsK+vr/FkQ==\",\"country\":\"UA\",\"language\":\"en\",\"api_version\":8.0,\"name\":\"Suzanna Rybtsova\",\"avatar\":\"https://media-direct.cdn.viber.com/download_photo?dlid=EhLSmQPRZ4hwe6ZLBa-ydQ8XxzK_bSBVBBNS2QZ0y1LGqFcBXIIqR_vAwsjqlu2TRnu-MQjGq3ZZZwNJe2pX3UcZhKyqhXXZ_3weOShaY8DRvrSQ6zMtcB6AJet36e4Cl9AXyg&fltp=jpg&imsz=0000\"}";
				//UserDetails viberUser = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(jsonString);

				UserDetails viberUser = await _viberBotClient.GetUserDetailsAsync(callback.Sender.Id);
				User newUser = _mapper.Map<User>(viberUser);
				await _userRepository.Add(newUser);


			}
		}

	}
}
