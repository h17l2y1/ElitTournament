using AutoMapper;
using ElitTournament.DAL.Entities;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.DAL.Repositories.Interfaces;
using ElitTournament.Telegram.BLL.Commands;
using ElitTournament.Telegram.BLL.Constants;
using ElitTournament.Telegram.BLL.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Telegram.BLL.Services
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly IMapper _mapper;
        private readonly ITelegramBotClient _telegramClient;
        private readonly IUserRepository _userRepository;
        private readonly ILeagueRepository _leagueRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IDataVersionRepository _dataVersionRepository;
        private readonly string _webHook;
        private readonly string _telegramUrl;
        private List<Command> _commands;
        
        public TelegramBotService(IConfiguration configuration, ITelegramBotClient telegramClient, IDataVersionRepository dataVersionRepository,
            IUserRepository userRepository, ILeagueRepository leagueRepository, IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _mapper = mapper;
            _telegramClient = telegramClient;
            _userRepository = userRepository;
            _leagueRepository = leagueRepository;
            _scheduleRepository = scheduleRepository;
            _dataVersionRepository = dataVersionRepository;
            _webHook = configuration["WebHook"];
            _telegramUrl = "/api/telegram/update";
        }
        
        public async Task SetWebhookAsync()
        {
            var telegramWebHook = $"{_webHook}{_telegramUrl}";
            await _telegramClient.SetWebhookAsync(telegramWebHook);
        }

        public async Task<IEnumerable<DAL.Entities.User>> GetAllUsers()
        {
            IEnumerable<DAL.Entities.User> users = await _userRepository.GetAll();
            return users;
        }

        private async Task InitCommands()
        {
            int lastVersion = await _dataVersionRepository.GetLastVersion();

            _commands = new List<Command>
            {
                // TODO: add validation
                //new ErrorCommand(),
                new WelcomCommand(lastVersion),
                new TableCommand(_leagueRepository, lastVersion),
                new DevelopCommand(_leagueRepository, lastVersion),
                new TeamsCommand(_leagueRepository, lastVersion),
                new ScheduleCommand(_scheduleRepository, lastVersion),
                new LeagueCommand(_leagueRepository, lastVersion)
            };
        }

        public async Task Update(Update update)
        {
            await GetUserDetails(update);
            await InitCommands();

            foreach (Command command in _commands)
            {
                bool isTeamExist = await command.Contains(update.Message.Text);
                if (isTeamExist)
                {
                    await command.Execute(update?.Message, _telegramClient);
                    break;
                }
            }
        }

        private async Task GetUserDetails(Update update)
        {
            bool userIsExist = await _userRepository.IsExist(update.Message.From.Id.ToString());
            if (!userIsExist)
            {
                var telegramUser = update.Message.From;
                var newUser = _mapper.Map<DAL.Entities.User>(telegramUser);
                await _userRepository.CreateAsync(newUser);
            }
        }


    }
}
