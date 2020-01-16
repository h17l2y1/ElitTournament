using ElitTournament.Domain.Commands;
using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Providers.Interfaces;
using ElitTournament.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Domain.Providers
{
    public class BotProvider : IBotProvider
    {
        private TelegramBotClient _client;
        private readonly string _key;
        private readonly string _botName;
        private readonly string _url;
        private List<Command> _commands;
        private readonly ICacheHelper _cacheHelper;

        public BotProvider(IConfiguration configuration, ICacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;
            _key = configuration["TelegramToken"];
            _botName = configuration["BotName"];
            _url = configuration["WebHook"];
        }
        public async Task InitializeClient()
        {
			//if (_client == null)
			//{
				_commands = new List<Command>
				{
					new StartCommand(_cacheHelper),
					new TeamsCommand(_cacheHelper),
					new ScheduleCommand(_cacheHelper)
				};

				_client = new TelegramBotClient(_key);
                try
                {
                    await _client.SetWebhookAsync(_url);
                }
                catch (Exception ex)
                {

                }
            //}
        }

        public async Task Update(Update update)
        {
            await InitializeClient();

            foreach (var command in _commands)
            {
				bool isTeamExist = command.Contains(update?.Message?.Text);
				if (isTeamExist)
                {
                    command.Execute(update?.Message, _client);
                    break;
                }
            }
        }
    }
}
