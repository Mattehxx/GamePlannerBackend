
using GamePlanner.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.Services
{
    public class UpdateLevelBackgroundService(IServiceProvider serviceProvider) : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<GamePlannerDbContext>();

                    var oneMinuteAgo = DateTime.Now.AddSeconds(-60);

                    var sessions = await dbContext.Sessions
                        .Where(s => s.EndDate > oneMinuteAgo && s.EndDate < DateTime.Now)
                        .ToListAsync(cancellationToken);

                    var tasks = sessions.Select(async session =>
                    {
                        try
                        {
                            var reservation = await dbContext.Reservations
                                .Include(r => r.User)
                                .FirstOrDefaultAsync(r => r.SessionId == session.SessionId, cancellationToken);

                            if (reservation?.User != null)
                            {
                                reservation.User.Level += (int)(session.EndDate - session.StartDate).TotalHours;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Session {session.SessionId} error: {ex.Message}");
                        }
                    });

                    await Task.WhenAll(tasks);

                    await dbContext.SaveChangesAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Update level background service error: {ex}");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }
    }
}
