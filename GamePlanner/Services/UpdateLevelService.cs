
using GamePlanner.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.Services
{
    public class UpdateLevelService(IServiceProvider serviceProvider) : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GamePlannerDbContext>();

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000 * 10, cancellationToken);

                try
                {
                    //var oneMinuteAgo = DateTime.Now.AddSeconds(-60);

                    //var reservations = await dbContext.GameSessions
                    //    .Include(gs => gs.Reservations)
                    //    .Where(gs => gs.GameSessionEndDate > oneMinuteAgo
                    //    && gs.GameSessionEndDate < DateTime.Now)
                    //    .SelectMany(gs => gs.Reservations)
                    //    .Include(r => r.User)
                    //    .ToListAsync(cancellationToken);

                    //foreach (var singleReservation in reservations)
                    //{
                    //    if (singleReservation.User is not null && singleReservation.GameSession is not null)
                    //    {
                    //        singleReservation.User.Level += (int)
                    //            (singleReservation.GameSession.GameSessionStartDate 
                    //            - singleReservation.GameSession.GameSessionEndDate).TotalHours;
                    //    }
                    //}

                    //await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
        }
    }
}
