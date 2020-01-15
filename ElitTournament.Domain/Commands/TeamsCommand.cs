using ElitTournament.Domain.Entities;
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
        public TeamsCommand() : base("all leages")
        {
            Text = "Выберите команду или нажмите назад если вы ошиблись лигой";
            Leagues = new List<League>();
            for (int i = 0; i < 10; i++)
            {
                var leageNum = i + 1;
                var league = new League($"Leage {leageNum}");
                for (int j = 0; j < 10; j++)
                {
                    var teamNum = j + 1;
                    league.Teams.Add($"Team {teamNum}");
                }
                Leagues.Add(league);
            }
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
            List<List<KeyboardButton>> keyBoard = new List<List<KeyboardButton>>();
            var currentLeague = Leagues.FirstOrDefault(p => p.Name == comand);
            var isEven = currentLeague.Teams.Count % 2;
            for (int i = 0; i < currentLeague.Teams.Count; i++)
            {
                List<KeyboardButton> team = new List<KeyboardButton>();
                if (isEven == 0)
                {
                    team.Add(new KeyboardButton(currentLeague.Teams[i]));
                    i++;
                    team.Add(new KeyboardButton(currentLeague.Teams[i]));
                }
                else
                {
                    team.Add(new KeyboardButton(currentLeague.Teams[i]));
                    i++;
                    if (i != Leagues.Count)
                    {
                        team.Add(new KeyboardButton(currentLeague.Teams[i]));
                    }
                }

                keyBoard.Add(team);
            }
            List<KeyboardButton> backButon = new List<KeyboardButton>();
            backButon.Add(new KeyboardButton("Назад"));
            keyBoard.Add(backButon);
            menu.Keyboard = keyBoard;

            return menu;
        }
    }
}
