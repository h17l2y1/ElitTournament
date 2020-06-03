using ElitTournament.Domain.Commands;
using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Domain.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly ICacheHelper _cacheHelper;
        private List<Command> _commands;

        public TelegramService(ICacheHelper cacheHelper, ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
            _cacheHelper = cacheHelper;
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

        public void Update(Update update)
        {
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
    }
}
