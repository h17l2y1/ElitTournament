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
        private readonly IUserRepository _userRepository;
        private readonly ITelegramBotClient _telegramClient;
        private readonly ICacheHelper _cacheHelper;
        private readonly string _webHook;
        private readonly string _telegramUrl;
        private List<Command> _commands;

        public TelegramBotService(IConfiguration configuration, ITelegramBotClient telegramClient, ICacheHelper cacheHelper,
            IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _telegramClient = telegramClient;
            _cacheHelper = cacheHelper;
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

        private void InitCommands()
        {
            _commands = new List<Command>
            {
                new ErrorCommand(),
                new DevelopCommand(_cacheHelper),
                new TeamsCommand(_cacheHelper),
                new ScheduleCommand(_cacheHelper),
                new LeagueCommand(_cacheHelper),
            };

            if (_cacheHelper.IsCacheExist)
            {
                _commands.RemoveAt(0);
            }
        }

        public async Task Update(Update update)
        {
            await GetUserDetails(update);

            if (update.Message.Text == ButtonConstant.START)
            {
                var c = new WelcomCommand();
                c.Execute(update?.Message, _telegramClient);
                return;
            }

            InitCommands();

            foreach (Command command in _commands)
            {
                bool isTeamExist = command.Contains(update?.Message?.Text);
                if (isTeamExist)
                {
                    command.Execute(update?.Message, _telegramClient);
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
