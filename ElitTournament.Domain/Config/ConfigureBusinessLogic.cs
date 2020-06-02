using ElitTournament.Domain.Helpers;
using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Providers;
using ElitTournament.Domain.Providers.Interfaces;
using ElitTournament.Domain.Services;
using ElitTournament.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace ElitTournament.Domain.Config
{
	public static class ConfigureBusinessLogic
	{
		public static void InjectBusinessLogicDependency(this IServiceCollection services)
		{
			// Services;
			services.AddScoped<IGrabberService, GrabberService>();
			services.AddScoped<IScheduleService, ScheduleService>();


			// Providers
			services.AddScoped<IGrabberProvider, GrabberProvider>();
			services.AddScoped<IViberProvider, ViberProvider>();

			// Helpers
			services.AddScoped<IHtmlLoaderHelper, HtmlLoaderHelper>();
			services.AddScoped<IGrabberHelper, GrabberHelper>();


			// Singleton
			services.AddSingleton<IBotProvider, BotProvider>();
			services.AddSingleton<ICacheHelper, CacheHelper>();

			services.AddHostedService<GrabberBackgroudRefreshService>();
		}

	}
}
