using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
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
        private readonly string _secondPossibleComand;
		private readonly ICacheHelper _cacheHelper;
		private readonly List<League> Leagues;

		public StartCommand(ICacheHelper cacheHelper) 
			: base("расписание")
        {
            _secondPossibleComand = "назад";
            _cacheHelper = cacheHelper;
			Leagues = new List<League>();
            for (int i = 0; i < 7; i++)
            {
                var leage = new League();
                var num = i + 1;
                leage.Name = $"Leage {num}";
                Leagues.Add(leage);
            }
            Text = @"Для просмотра расписание выберите лигу, а потом команду.";
        }

        public async override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            await client.SendTextMessageAsync(chatId, Text, ParseMode.Html, false, false, 0, GetMenu());
        }

        public override bool Contains(string command)
        {
           return command.ToLower().Contains(this.Name.ToLower()) || command.ToLower().Contains(_secondPossibleComand);
        }

        private ReplyKeyboardMarkup GetMenu()
        {
           
            var menu = new ReplyKeyboardMarkup();
            //_cacheHelper.Update();
            List<List<KeyboardButton>> list = new List<List<KeyboardButton>>();
            var isEven = Leagues.Count % 2;

            for (int i = 0; i < Leagues.Count; i++)
			{
                List<KeyboardButton> test = new List<KeyboardButton>();
                if (isEven == 0)
                {                    
                    test.Add(new KeyboardButton(Leagues[i].Name));
                    i++;
                    test.Add(new KeyboardButton(Leagues[i].Name));
                }
                else
                {
                    test.Add(new KeyboardButton(Leagues[i].Name));
                    i++;
                    if(i!= Leagues.Count)
                    {
                        test.Add(new KeyboardButton(Leagues[i].Name));
                    }
                }

                list.Add(test);
			}

			menu.Keyboard = list;
            return menu;
        }
    }
}
