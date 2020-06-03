using ElitTournament.Core.Helpers;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Core.Providers;
using ElitTournament.Core.Providers.Interfaces;
using ElitTournament.Core.Services;
using ElitTournament.Core.Services.Interfaces;
using ElitTournament.Telegram.BLL.Services;
using ElitTournament.Telegram.BLL.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace ElitTournament.Telegram.BLL.Config
{
	public static class ConfigureBusinessLogic
	{
		public static void InjectBusinessLogicDependency(this IServiceCollection services, IConfiguration configuration)
		{
			InitTelegramBotClient(services, configuration);
			AddDependency(services);
		}

		private static void InitTelegramBotClient(IServiceCollection services, IConfiguration configuration)
		{
			ITelegramBotClient telegramClient = new TelegramBotClient(configuration["Token"]);
			services.AddSingleton(telegramClient);
		}

		public static void AddDependency(IServiceCollection services)
		{
			// Services;
			//services.AddScoped<ITelegramBotService, TelegramBotService>();
			services.AddScoped<IGrabberService, GrabberService>();
			services.AddScoped<IScheduleService, ScheduleService>();


			// Providers
			services.AddScoped<IGrabberProvider, GrabberProvider>();

			// Helpers
			services.AddScoped<IHtmlLoaderHelper, HtmlLoaderHelper>();
			services.AddScoped<IGrabberHelper, GrabberHelper>();

			// Singleton
			services.AddSingleton<ITelegramBotService, TelegramBotService>();
			services.AddSingleton<ICacheHelper, CacheHelper>();

			services.AddHostedService<GrabberBackgroudRefreshService>();
		}

	}
}
