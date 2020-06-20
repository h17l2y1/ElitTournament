using AutoMapper;
using ElitTournament.Core.Helpers;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Core.Providers;
using ElitTournament.Core.Providers.Interfaces;
using ElitTournament.Core.Services;
using ElitTournament.Core.Services.Interfaces;
using ElitTournament.DAL.Config;
using ElitTournament.Telegram.BLL.Services;
using ElitTournament.Telegram.BLL.Services.Interfaces;
using Imgur.API.Authentication;
using Imgur.API.Authentication.Impl;
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
			InitImgurClient(services, configuration);
			AddAutoMapper(services);
			AddDependency(services);

			services.InjectDataAccessDependency(configuration);
		}

		private static void InitTelegramBotClient(IServiceCollection services, IConfiguration configuration)
		{
			ITelegramBotClient telegramClient = new TelegramBotClient(configuration["Token"]);
			services.AddSingleton(telegramClient);
		}

		private static void InitImgurClient(IServiceCollection services, IConfiguration configuration)
		{
			IImgurClient imgurClient = new ImgurClient(configuration["Imgur:Id"], configuration["Imgur:Secret"]);
			services.AddSingleton(imgurClient);
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
			services.AddScoped<ITelegramBotService, TelegramBotService>();

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
