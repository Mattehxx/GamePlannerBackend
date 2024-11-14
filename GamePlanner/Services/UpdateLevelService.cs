
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
                    var oneMinuteAgo = DateTime.Now.AddSeconds(-60);

                    var sessions = await dbContext.Sessions
                        .Where(s => s.EndDate > oneMinuteAgo && s.EndDate < DateTime.Now)
                        .ToListAsync(cancellationToken);

                    foreach (var session in sessions)
                    {
                        var reservation = await dbContext.Reservations
                            .Include(r => r.User)
                            .FirstOrDefaultAsync(r => r.SessionId == session.SessionId, cancellationToken);

                        if (reservation is not null && reservation.User is not null)
                        {
                            reservation.User.Level += (int)(session.StartDate - session.EndDate).TotalHours;
                        }
                    }

                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
        }
    }
}
