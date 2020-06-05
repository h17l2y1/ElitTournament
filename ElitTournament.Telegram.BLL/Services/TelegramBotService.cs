﻿using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Telegram.BLL.Commands;
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
        private readonly ITelegramBotClient _telegramClient;
        private readonly ICacheHelper _cacheHelper;
        private readonly string _webHook;
        private readonly string _telegramUrl;
        private List<Command> _commands;

        public TelegramBotService(IConfiguration configuration, ITelegramBotClient telegramClient, ICacheHelper cacheHelper)
        {
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