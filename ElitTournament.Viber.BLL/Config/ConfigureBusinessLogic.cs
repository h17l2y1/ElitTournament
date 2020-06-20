using AutoMapper;
using ElitTournament.Core.Helpers;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Core.Providers;
using ElitTournament.Core.Providers.Interfaces;
using ElitTournament.Core.Services;
using ElitTournament.Core.Services.Interfaces;
using ElitTournament.DAL.Config;
using ElitTournament.Viber.BLL.Services;
using ElitTournament.Viber.BLL.Services.Interfaces;
using ElitTournament.Viber.Core;
using ElitTournament.Viber.Core.Models.Interfaces;
using Imgur.API.Authentication;
using Imgur.API.Authentication.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElitTournament.Viber.BLL.Config
{
	public static class ConfigureBusinessLogic
	{
		public static void InjectBusinessLogicDependency(this IServiceCollection services, IConfiguration configuration)
		{
			InitViberBotClient(services, configuration);
			InitImgurClient(services, configuration);
			AddAutoMapper(services);
			AddDependency(services);

			services.InjectDataAccessDependency(configuration);
		}

		private static void InitViberBotClient(IServiceCollection services, IConfiguration configuration)
		{
			IViberBotClient viberClient = new ViberBotClient(configuration["Token"]);
			services.AddSingleton(viberClient);
		}

		private static void InitImgurClient(IServiceCollection services, IConfiguration configuration)
		{
			IImgurClient imgeurClient = new ImgurClient(configuration["Imgur:Id"], configuration["Imgur:Secret"]);
			services.AddSingleton(imgeurClient);
		}
		
		private static void AddAutoMapper(IServiceCollection services)
		{
			var config = new MapperConfiguration(c =>
			{
				c.AddProfile(new MapperProfile());
			});

			IMapper mapper = config.CreateMapper();

			services.AddSingleton(mapper);
		}

		private static void AddDependency(IServiceCollection services)
		{
			// Services;
			services.AddScoped<IGrabberService, GrabberService>();
			services.AddScoped<IViberBotService, ViberBotService>();
			
			// Providers
			services.AddScoped<IGrabberProvider, GrabberProvider>();
			
			// Helpers
			services.AddScoped<IHtmlLoaderHelper, HtmlLoaderHelper>();
			services.AddScoped<IGrabberHelper, GrabberHelper>();
			services.AddScoped<IImageHelper, ImageHelper>();
			
			services.AddHostedService<GrabberBackgroudRefreshService>();
		}

	}
}
