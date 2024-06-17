using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using EvonaZadatak.Contracts;

public class DailyAwardService : BackgroundService
{
    private readonly IServiceProvider _services;

    public DailyAwardService(IServiceProvider services)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int awardId = 19;
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _services.CreateScope())
            {
                var userAwardService = scope.ServiceProvider.GetRequiredService<IUserAward>();

               
                await userAwardService.AddAwardToAllUsers(awardId); 
            }

           
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);

            //await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}