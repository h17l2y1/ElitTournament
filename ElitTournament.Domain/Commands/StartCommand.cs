using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ElitTournament.Domain.Commands
{
    public class StartCommand : Command
    {
        private readonly string _firstPossibleComand;
        private readonly string _secondPossibleComand;
		private readonly ICacheHelper _cacheHelper;
		private readonly List<League> Leagues;

		public StartCommand(ICacheHelper cacheHelper) 
			: base("расписание")
        {
            _firstPossibleComand = "Начать";
            _secondPossibleComand = "назад";
            _cacheHelper = cacheHelper;
			Leagues = new List<League>();
            Leagues = _cacheHelper.GetLeagues();
            Text = @"Для просмотра расписание выберите лигу, а потом команду.";
        }

        public async override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            await client.SendTextMessageAsync(chatId, Text, ParseMode.Html, false, false, 0, GetMenu());
        }

        public override bool Contains(string command)
        {
           return command.ToLower().Contains(Name.ToLower()) 
				|| command.ToLower().Contains(_firstPossibleComand)
				|| command.ToLower().Contains(_secondPossibleComand);
        }

        private ReplyKeyboardMarkup GetMenu()
        {        
            var menu = new ReplyKeyboardMarkup();
            List<List<KeyboardButton>> list = new List<List<KeyboardButton>>();
            if (Leagues != null)
            {
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
                        if (i != Leagues.Count)
                        {
                            test.Add(new KeyboardButton(Leagues[i].Name));
                        }
                    }

                    list.Add(test);
                }
            }

            menu.Keyboard = list;
            return menu;
        }
    }
}
