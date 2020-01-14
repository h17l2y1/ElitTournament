﻿using ElitTournament.Domain.Config;
using ElitTournament.Domain.Providers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElitTournament.Api
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
			services.InjectBusinessLogicDependency();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			var serviceProvider = app.ApplicationServices;
			var bot = serviceProvider.GetService<IBotProvider>();
			bot.InitializeClient().Wait();

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
