using ElitTournament.Viber.Api.Middleware;
using ElitTournament.Viber.BLL.Config;
using ElitTournament.Viber.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ElitTournament.Viber.Api
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

			//loggerFactory.AddFile(Configuration.GetSection("Logging"));
			app.UseHttpStatusCodeExceptionMiddleware();
			app.UseMiddleware<ErrorHandlingMiddleware>();

			//IServiceProvider serviceProvider = app.ApplicationServices;
			//IViberBotService bot = serviceProvider.GetService<IViberBotService>();
			//bot.SetWebHookAsync().Wait();


			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
