﻿using ElitTournament.Domain.Commands;
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
            if (_client == null)
            {
                _commands = new List<Command>();
                _commands.Add(new StartCommand(_cacheHelper));
                _commands.Add(new TeamsCommand());
                _commands.Add(new ScheduleCommand(_cacheHelper));

                _client = new TelegramBotClient(_key);
                try
                {
                    await _client.SetWebhookAsync(_url);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task Update(Update update)
        {
            await InitializeClient();

            foreach (var command in _commands)
            {
                if (command.Contains(update?.Message?.Text))
                {
                    command.Execute(update?.Message, _client);
                    break;
                }
            }
        }
    }
}
