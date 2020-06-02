using ElitTournament.Domain.Commands;
using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Providers.Interfaces;
using ElitTournament.Viber.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Domain.Providers
{
    public class BotProvider : IBotProvider
    {
        private TelegramBotClient _telegramClient;
        private ViberBotClient _viberClient;
        private readonly string _telegramToken;
        private readonly string _viberToken;
        private readonly string _botName;
        private readonly string _telegramWebHook;
        private readonly string _viberWebHook;
        private List<Command> _commands;
        private readonly ICacheHelper _cacheHelper;

        public BotProvider(IConfiguration configuration, ICacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;
            _telegramToken = configuration["TelegramToken"];
            _viberToken = configuration["ViberToken"];
            _botName = configuration["BotName"];
            _telegramWebHook = configuration["TelegramWebHook"];
            _viberWebHook = configuration["ViberWebHook"];
        }

        public async Task InitializeClient()
        {
            InitCommands();
            await InitTelegramBot();
            await InitViberBot();
        }

        private void InitCommands()
        {
            _commands = new List<Command>
            {
                new StartCommand(_cacheHelper),
                new TeamsCommand(_cacheHelper),
                new ScheduleCommand(_cacheHelper)
            };
        }

        private async Task InitTelegramBot()
        {
            _telegramClient = new TelegramBotClient(_telegramToken);
            await _telegramClient.SetWebhookAsync(_telegramWebHook);
        }

        private async Task InitViberBot()
        {
            _viberClient = new ViberBotClient(_viberToken);
            await _viberClient.SetWebhookAsync(_viberWebHook);
        }

        public async Task Update(Update update)
        {
            await InitializeClient();

            foreach (var command in _commands)
            {
                bool isTeamExist = command.Contains(update?.Message?.Text);
                if (isTeamExist)
                {
                    command.Execute(update?.Message, _telegramClient);
                    break;
                }
            }
        }
    }
}
