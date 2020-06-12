using ElitTournament.DAL.Entities;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Telegram.BLL.Constants;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ElitTournament.Telegram.BLL.Commands
{
    public class LeagueCommand : Command
    {
        private readonly ICacheHelper _cacheHelper;

        public LeagueCommand(ICacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;
        }

        public override bool Contains(string command)
        {
            return true;
        }

        public async override void Execute(Message message, ITelegramBotClient client)
        {
            string text = MessageConstant.CHOOSE_LEAGUE;
            if (message.Text == MessageConstant.DEVELOP)
            {
                text = "В разработке: \n1. Турнирные таблицы \n2. Бот будет сам уведомлять когда игра\n\nСвязь с разработчиком\n0955923228";
            }

            await client.SendTextMessageAsync(message.Chat.Id, text, ParseMode.Html, false, false, 0, GetMenu());
        }

        private ReplyKeyboardMarkup GetMenu()
        {
            List<League> leagues = _cacheHelper.GetLeagues();

            var menu = new ReplyKeyboardMarkup();
            List<List<KeyboardButton>> list = new List<List<KeyboardButton>>();

            if (leagues != null)
            {
                var isEven = leagues.Count % 2;

                for (int i = 0; i < leagues.Count; i++)
                {
                    List<KeyboardButton> test = new List<KeyboardButton>();
                    if (isEven == 0)
                    {
                        test.Add(new KeyboardButton(leagues[i].Name));
                        i++;
                        test.Add(new KeyboardButton(leagues[i].Name));
                    }
                    else
                    {
                        test.Add(new KeyboardButton(leagues[i].Name));
                        i++;
                        if (i != leagues.Count)
                        {
                            test.Add(new KeyboardButton(leagues[i].Name));
                        }
                    }

                    list.Add(test);
                }
            }

            list.Add(new List<KeyboardButton> { new KeyboardButton(MessageConstant.DEVELOP) });
            menu.Keyboard = list;

            return menu;
        }
    }
}
