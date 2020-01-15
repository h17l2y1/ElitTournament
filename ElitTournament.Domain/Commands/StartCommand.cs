using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ElitTournament.Domain.Commands
{
    public class StartCommand : Command
    {
		private readonly IScheduleService _scheduleService;
		private readonly List<League> Leagues;

		public StartCommand(IScheduleService scheduleService) 
			: base("расписание")
        {
			_scheduleService = scheduleService;
			Leagues = new List<League>();
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


			//List<IEnumerable<KeyboardButton>> list = new List<IEnumerable<KeyboardButton>>();
			List<List<KeyboardButton>> list = new List<List<KeyboardButton>>();

			for (int i = 0; i < Leagues.Count; i++)
			{
				List<KeyboardButton> test = new List<KeyboardButton>
				{
					new KeyboardButton(Leagues[i].Name),
					new KeyboardButton(Leagues[i + 1].Name)
				};

				//IEnumerable<KeyboardButton> enu = test.AsEnumerable();

				list.Add(test);
			}
			//IEnumerable<IEnumerable<KeyboardButton>> enu1 = list.AsEnumerable();

			menu.Keyboard = list;

			//menu.Keyboard = new[]
   //         {
   //             new[]{
   //             new KeyboardButton("Elit-лига"),
   //             new KeyboardButton("1-лига"),               
   //             },
   //             new[]{
   //                 new KeyboardButton("2-лига группа А"),
   //                 new KeyboardButton("2-лига группа Б"),
   //             },
   //             new[]{
   //                 new KeyboardButton("3-лига"),
   //                 new KeyboardButton("4-лига"),
   //             },

   //             new[]{
   //                 new KeyboardButton("5-лига"),
   //                 new KeyboardButton("6-лига"),
   //             },
   //             new[]{
   //                 new KeyboardButton("7-лига"),
   //                 new KeyboardButton("8-лига"),
   //             },
   //             new[]{
   //                 new KeyboardButton("9-лига"),
   //                 new KeyboardButton("10-лига"),
   //             },
   //         };

            return menu;
        }
    }
}
