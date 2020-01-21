using ElitTournament.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Services
{
    public class GrabberBackgroudRefreshService : BackgroundService
    {
        public IServiceProvider Services { get; }

        public GrabberBackgroudRefreshService(IServiceProvider services)
        {
            Services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = Services.CreateScope())
                {
                    var scopedProcessingService =
                        scope.ServiceProvider
                            .GetRequiredService<IGrabberService>();

                    await scopedProcessingService.GrabbElitTournament();
                }
                int hour = 60000;
                int day = 1440000;
                await Task.Delay(hour, cancellationToken);
            }
        }
    }
}
