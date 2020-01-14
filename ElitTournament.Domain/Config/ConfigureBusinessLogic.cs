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
			//Automapper setup
			//var config = new AutoMapper.MapperConfiguration(c =>
			//{
			//	c.AddProfile(new MapperProfile());
			//});
			//var mapper = config.CreateMapper();
			//services.AddSingleton(mapper);


			// Services;
			services.AddScoped<IScheduleService, ScheduleService>();


			// Providers;
			services.AddScoped<IScheduleProvider, ScheduleProvider>();
			services.AddScoped<IScoreProvider, ScoreProvider>();
			services.AddSingleton<IBotProvider, BotProvider>();


			// Helpers;
			services.AddScoped<IHtmlLoaderHelper, HtmlLoaderHelper>();
			services.AddScoped<IScheduleHelper, ScheduleHelper>();
			services.AddScoped<IScoreHelper, ScoreHelper>();

			services.AddScoped<ICacheHelper, CacheHelper>();


		}

	}
}
