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
using System.Linq;
using System.Threading.Tasks;
using ElitTournament.Viber.BLL.Constants;
using ElitTournament.Viber.Core.Models.Message;

namespace ElitTournament.Viber.BLL.Services
{
	public class ViberBotService : IViberBotService
	{
		private readonly IMapper _mapper;
		private readonly IViberBotClient _viberBotClient;
		private readonly IUserRepository _userRepository;
		private readonly ILeagueRepository _leagueRepository;
		private readonly IScheduleRepository _scheduleRepository;
		private readonly IDataVersionRepository _dataVersionRepository;
		private readonly IImageHelper _imageHelper;
		private readonly string _webHook;
		private readonly string _viberUrl;
		private List<Command> _commands;

		public ViberBotService(IConfiguration configuration, IViberBotClient viberBotClient, IDataVersionRepository dataVersionRepository, IImageHelper imageHelper,
			IUserRepository userRepository, ILeagueRepository leagueRepository, IScheduleRepository scheduleRepository, IMapper mapper)
		{
			_mapper = mapper;
			_viberBotClient = viberBotClient;
			_userRepository = userRepository;
			_leagueRepository = leagueRepository;
			_scheduleRepository = scheduleRepository;
			_dataVersionRepository = dataVersionRepository;
			_imageHelper = imageHelper;
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

		public async Task SendBroadcastMessage()
		{
			IEnumerable<User> users = await _userRepository.GetAll();
			IEnumerable<string> viberUserIds = users.Where(w=>w.IsViber).Select(x => x.ClientId).ToList();
			int lastVersion = await _dataVersionRepository.GetLastVersion();

			BroadcastMessage broadcastMessage = new BroadcastMessage("oFKwijuinRXIqIUwBvbpEw==");
			broadcastMessage.Sender = new UserBase
			{
				Name = MessageConstant.BOT_NAME, Avatar = MessageConstant.BOT_AVATAR
			};
			broadcastMessage.BroadcastList = viberUserIds;
			broadcastMessage.MinApiVersion = 2;
			broadcastMessage.Text = MessageConstant.UPDATE;

			await _viberBotClient.SendBroadcastMessageAsync(broadcastMessage);

			LeaguesCommand leaguesCommand = new LeaguesCommand(_leagueRepository, lastVersion);

			foreach (var id in viberUserIds)
			{
				var callback = new Callback();
				callback.Sender = new UserModel();
				callback.Sender.Id = id;
				await leaguesCommand.Execute(callback, _viberBotClient);
			}
			
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
			}
		}

		private async Task InitCommands()
		{
			int lastVersion = await _dataVersionRepository.GetLastVersion();
			
			_commands = new List<Command>
			{
				// TODO: add validation
				//new ErrorCommand(),
				new TableCommand(_leagueRepository, _imageHelper, lastVersion),
				new DevelopCommand(_leagueRepository, lastVersion),
				new TeamsCommand(_leagueRepository, lastVersion),
				new ScheduleCommand(_scheduleRepository, _leagueRepository, lastVersion),
				new LeaguesCommand(_leagueRepository, lastVersion)
			};
		}

		private async Task SendMessage(Callback callback)
		{
			await InitCommands();

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
				UserDetails viberUser = await _viberBotClient.GetUserDetailsAsync(callback.Sender.Id);
				User newUser = _mapper.Map<User>(viberUser);
				await _userRepository.CreateAsync(newUser);
			}
		}

	}
}
