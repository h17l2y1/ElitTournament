﻿using ElitTournament.Domain.Helpers;
using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Providers;
using ElitTournament.Domain.Providers.Interfaces;
using ElitTournament.Domain.Services;
using ElitTournament.Domain.Services.Interfaces;
using ElitTournament.Viber.Core;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace ElitTournament.Domain.Config
{
	public static class ConfigureBusinessLogic
	{
		public static void InjectBusinessLogicDependency(this IServiceCollection services, IConfiguration configuration)
		{
			InitBots(services, configuration);
			AddDependency(services);
		}

		private static void InitBots(IServiceCollection services, IConfiguration configuration)
		{
			ITelegramBotClient telegramClient = new TelegramBotClient(configuration["TelegramToken"]);
			IViberBotClient viberClient = new ViberBotClient(configuration["ViberToken"]);

			services.AddSingleton(telegramClient);
			services.AddSingleton(viberClient);
		}

		public static void AddDependency(IServiceCollection services)
		{
			// Services;
			services.AddScoped<IGrabberService, GrabberService>();
			services.AddScoped<IScheduleService, ScheduleService>();
			services.AddScoped<ITelegramService, TelegramService>();


			// Providers
			services.AddScoped<IGrabberProvider, GrabberProvider>();
			services.AddScoped<IViberProvider, ViberProvider>();

			// Helpers
			services.AddScoped<IHtmlLoaderHelper, HtmlLoaderHelper>();
			services.AddScoped<IGrabberHelper, GrabberHelper>();

			// Singleton
			services.AddSingleton<IBotService, BotService>();
			services.AddSingleton<ICacheHelper, CacheHelper>();

			services.AddHostedService<GrabberBackgroudRefreshService>();
		}

	}
}
