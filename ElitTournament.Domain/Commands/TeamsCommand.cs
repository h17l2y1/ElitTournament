using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ElitTournament.Domain.Commands
{
    public class TeamsCommand : Command
    {
        private readonly List<League> Leagues;
        private readonly ICacheHelper _cacheHelper;

        public TeamsCommand(ICacheHelper cacheHelper) : base("all leages")
        {
            _cacheHelper = cacheHelper;
            Text = "Выберите команду или нажмите назад если вы ошиблись лигой";
            Leagues = new List<League>();
            Leagues = _cacheHelper.GetLeagues();
        }

        public async override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            
            await client.SendTextMessageAsync(chatId, Text, ParseMode.Html, false, false, 0, GetMenu(message.Text));
        }

        public override bool Contains(string command)
        {
            var leages = Leagues.Select(p => p.Name).ToList();
            return leages.Contains(command);
        }

        private ReplyKeyboardMarkup GetMenu(string comand)
        {
            var menu = new ReplyKeyboardMarkup();
            League currentLeague = Leagues.FirstOrDefault(p => p.Name == comand);

            List<List<KeyboardButton>> buttons = CreateButtons(currentLeague);

            menu.Keyboard = buttons;

            return menu;
        }

        private List<List<KeyboardButton>> CreateButtons(League currentLeague)
        {
            List<List<KeyboardButton>> keyBoard = new List<List<KeyboardButton>>();

            int teamsCount = currentLeague.Teams.Count();
            bool isEven = teamsCount % 2 == 0 ? true : false;

            for (int i = 0; i < teamsCount; i++)
            {
                List<KeyboardButton> teams = new List<KeyboardButton>();

                if (isEven)
                {
                    teams.Add(new KeyboardButton(currentLeague.Teams[i]));
                    i++;
                    teams.Add(new KeyboardButton(currentLeague.Teams[i]));
                }
                else
                {
                    teams.Add(new KeyboardButton(currentLeague.Teams[i]));
                    i++;
                    if (i != currentLeague.Teams.Count)
                    {
                        teams.Add(new KeyboardButton(currentLeague.Teams[i]));
                    }
                }

                keyBoard.Add(teams);
            }

            keyBoard.Add(new List<KeyboardButton> { new KeyboardButton("Назад") });
            return keyBoard;
        }
    }
}
