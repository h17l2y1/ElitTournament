using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Domain.Commands
{
	public class ScheduleCommand : Command
	{
		private List<League> _leagues;
		private ICacheHelper _cacheHelper;

		public ScheduleCommand(ICacheHelper cacheHelper) : base("")
		{
			_cacheHelper = cacheHelper;
			_leagues = _cacheHelper.GetLeagues();
		}

		public override bool Contains(string text)
		{
			bool isExistTeam = false;
			if (_leagues != null)
			{
				List<string> teams = _cacheHelper.GetTeams();

				string teamName = text.Replace("-", " ").ToUpper();

				string spaceTeam = teams.FirstOrDefault(x => x == teamName);

				if (spaceTeam != null )
				{
					isExistTeam = true;
				}
			}
			return isExistTeam;
		}

		public async override void Execute(Message message, TelegramBotClient client)
		{
			List<string> schedule = FindGame(message.Text);
			long chatId = message.Chat.Id;

			if (schedule == null)
			{
				string notFound = $"Не получилось получить расписание игр, поищите свою игру тут http://elitturnir.info/raspisanie/";
				await client.SendTextMessageAsync(chatId, notFound);
				return;
			}

			if (schedule.Count == 0)
			{
				string notFound = $"Игры команды \"{message.Text}\" не найдено";
				await client.SendTextMessageAsync(chatId, notFound);
				return;
			}

			var result = String.Join("\n\n", schedule.ToArray());

			await client.SendTextMessageAsync(chatId, result);
		}

		public List<string> FindGame(string teamName)
		{
			List<Schedule> schedule = _cacheHelper.GetSchedule();
			if (schedule != null && schedule.Count != 0)
			{
				List<string> list = new List<string>();
				string teamWithSpace = teamName.Replace("-", " ").ToUpper();

				foreach (var place in schedule)
				{
					foreach (var game in place.Games)
					{
						string gameString = game.Replace("-"," ").ToUpper();
						if (gameString.Contains(teamWithSpace))
						{
							list.Add($"{place.Place}\n{game}");
						}
					}
				}

				return list;
			}

			return null;
		}
	}
}
