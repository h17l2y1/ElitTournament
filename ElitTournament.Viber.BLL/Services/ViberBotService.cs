using AutoMapper;
using ElitTournament.DAL.Entities;
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
		private readonly IViberBotClient _viberBotClient;
		private readonly IUserRepository _userRepository;
		private readonly ITeamRepository _teamRepository;
		private readonly ILeagueRepository _leagueRepository;
		private readonly IScheduleRepository _scheduleRepository;
		private readonly string _webHook;
		private readonly string _viberUrl;
		private List<Command> _commands;

		public ViberBotService(IConfiguration configuration, IViberBotClient viberBotClient, ITeamRepository teamRepository, 
			IUserRepository userRepository, ILeagueRepository leagueRepository, IScheduleRepository scheduleRepository, IMapper mapper)
		{
			_mapper = mapper;
			_viberBotClient = viberBotClient;
			_userRepository = userRepository;
			_teamRepository = teamRepository;
			_leagueRepository = leagueRepository;
			_scheduleRepository = scheduleRepository;
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
				await ConversationStarted(callback);
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
				//new ErrorCommand(),
				new DevelopCommand(_leagueRepository),
				new TeamsCommand(_leagueRepository),
				new ScheduleCommand(_scheduleRepository, _leagueRepository),
				new LeaguesCommand(_leagueRepository),
			};
		}

		private async Task SendMessage(Callback callback)
		{
			InitCommands();

			foreach (Command command in _commands)
			{
				bool isTeamExist = await command.Contains(callback?.Message?.Text.Trim());
				if (isTeamExist)
				{
					await command.Execute(callback, _viberBotClient);
					break;
				}
			}
		}

		private async Task ConversationStarted(Callback callback)
		{
			var command = new WelcomCommand();
			await command.Execute(callback, _viberBotClient);
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
				await _userRepository.CreateAsync(newUser);


			}
		}

	}
}
