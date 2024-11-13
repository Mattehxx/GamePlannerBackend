
using GamePlanner.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.Services
{
    public class UpdateLevelService(IServiceProvider serviceProvider) : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000 * 60, cancellationToken);

                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<GamePlannerDbContext>();

                var reservations = await dbContext.GameSessions
                    .Include(gs => gs.Reservations)?.ThenInclude(r => r.User)
                    .Where(gs => gs.GameSessionEndDate > DateTime.Now.AddSeconds(-60)
                    && gs.GameSessionEndDate < DateTime.Now)
                    .SelectMany(gs => gs.Reservations)
                    .ToListAsync(cancellationToken);

                foreach (var singleReservation in reservations)
                {
                    if (singleReservation.User is not null && singleReservation.GameSession is not null)
                    {
                        singleReservation.User.Level += (int)
                            (singleReservation.GameSession.GameSessionStartDate 
                            - singleReservation.GameSession.GameSessionEndDate).TotalHours;
                    }
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
