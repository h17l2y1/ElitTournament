using ElitTournament.Core.Entities;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Telegram.BLL.Constants;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ElitTournament.Telegram.BLL.Commands
{
    public class TeamsCommand : Command
    {
        private readonly ICacheHelper _cacheHelper;

        public TeamsCommand(ICacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;
        }

        public override bool Contains(string command)
        {
            var leages = _cacheHelper.GetLeagues().Select(p => p.Name).ToList();
            return leages.Contains(command);
        }

        public async override void Execute(Message message, ITelegramBotClient client)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = GetMenu(message.Text);
            await client.SendTextMessageAsync(message.Chat.Id, MessageConstant.CHOOSE_TEAM, ParseMode.Html, false, false, 0, replyKeyboardMarkup);
        }

        private ReplyKeyboardMarkup GetMenu(string comand)
        {
            List<League> leagues = _cacheHelper.GetLeagues();

            var menu = new ReplyKeyboardMarkup();
            League currentLeague = leagues.FirstOrDefault(p => p.Name == comand);

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
