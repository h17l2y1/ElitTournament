using ElitTournament.Core.Helpers;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Core.Providers;
using ElitTournament.Core.Providers.Interfaces;
using ElitTournament.Core.Services;
using ElitTournament.Core.Services.Interfaces;
using ElitTournament.Viber.BLL.Services;
using ElitTournament.Viber.BLL.Services.Interfaces;
using ElitTournament.Viber.Core;
using ElitTournament.Viber.Core.Models.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElitTournament.Viber.BLL.Config
{
	public static class ConfigureBusinessLogic
	{
		public static void InjectBusinessLogicDependency(this IServiceCollection services, IConfiguration configuration)
		{
			InitViberBotClient(services, configuration);
			AddDependency(services);
		}

		private static void InitViberBotClient(IServiceCollection services, IConfiguration configuration)
		{
			IViberBotClient viberClient = new ViberBotClient(configuration["Token"]);
			services.AddSingleton(viberClient);
		}

		public static void AddDependency(IServiceCollection services)
		{
			// Services;
			services.AddScoped<IGrabberService, GrabberService>();
			services.AddScoped<IScheduleService, ScheduleService>();


			// Providers
			services.AddScoped<IGrabberProvider, GrabberProvider>();


			// Helpers
			services.AddScoped<IHtmlLoaderHelper, HtmlLoaderHelper>();
			services.AddScoped<IGrabberHelper, GrabberHelper>();


			// Singleton
			services.AddSingleton<IViberBotService, ViberBotService>();
			services.AddSingleton<ICacheHelper, CacheHelper>();


			services.AddHostedService<GrabberBackgroudRefreshService>();
		}

	}
}
