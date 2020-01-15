using ElitTournament.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ElitTournament.Domain.Commands
{
    public class StartCommand : Command
    {
		private readonly IScheduleService _scheduleService;

		public StartCommand(IScheduleService scheduleService) 
			: base("расписание")
        {
			_scheduleService = scheduleService;
			Text = @"Для просмотра расписание выберите лигу, а потом команду.";
        }

        public async override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            await client.SendTextMessageAsync(chatId, Text, ParseMode.Html, false, false, 0, GetMenu());
        }

        private ReplyKeyboardMarkup GetMenu()
        {
            var menu = new ReplyKeyboardMarkup();
            menu.Keyboard = new[]
            {
                new[]{
                new KeyboardButton("Elit-лига"),
                new KeyboardButton("1-лига"),               
                },
                new[]{
                    new KeyboardButton("2-лига группа А"),
                    new KeyboardButton("2-лига группа Б"),
                },
                new[]{
                    new KeyboardButton("3-лига"),
                    new KeyboardButton("4-лига"),
                },

                new[]{
                    new KeyboardButton("5-лига"),
                    new KeyboardButton("6-лига"),
                },
                new[]{
                    new KeyboardButton("7-лига"),
                    new KeyboardButton("8-лига"),
                },
                new[]{
                    new KeyboardButton("9-лига"),
                    new KeyboardButton("10-лига"),
                },
            };

            return menu;
        }
    }
}
