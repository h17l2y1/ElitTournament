using ElitTournament.Domain.Services.Interfaces;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ElitTournament.Domain.Services
{
	public class BotService : IBotService
	{
        private readonly ITelegramBotClient _telegramClient;
        private readonly IViberBotClient _viberClient;
        private readonly string _webHook;
        private readonly string _telegramUrl;
        private readonly string _viberUrl;

        public BotService(IConfiguration configuration, ITelegramBotClient telegramClient, IViberBotClient viberClient)
        {
            _telegramClient = telegramClient;
            _viberClient = viberClient;
            _webHook = configuration["WebHook"];
            _telegramUrl = "/api/telegram/update";
            _viberUrl = "/api/viber/update";
        }

        public async Task InitializeClients()
        {
            await InitTelegramBot();
            await InitViberBot();
        }

        private async Task InitTelegramBot()
        {
            var telegramWebHook = $"{_webHook}{_telegramUrl}";
            await _telegramClient.SetWebhookAsync(telegramWebHook);
        }

        private async Task InitViberBot()
        {
            var viberWebHook = $"{_webHook}{_viberUrl}";
            await _viberClient.SetWebhookAsync(viberWebHook);
        }

    }
}
