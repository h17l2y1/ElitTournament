﻿using ElitTournament.Telegram.Api.Middleware;
using ElitTournament.Telegram.BLL.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ElitTournament.Telegram.Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; }


		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.InjectBusinessLogicDependency(Configuration);

			services.AddMemoryCache();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			loggerFactory.AddFile(Configuration.GetSection("Logging"));
			app.UseHttpStatusCodeExceptionMiddleware();
			app.UseMiddleware<ErrorHandlingMiddleware>();

			//IServiceProvider serviceProvider = app.ApplicationServices;
			//ITelegramBotService bot = serviceProvider.GetService<ITelegramBotService>();
			//bot.SetWebhookAsync().Wait();


			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
