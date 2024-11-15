using GamePlanner.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.Services
{
    public class ReservationTokenBackgroundService(IServiceProvider serviceProvider) : BackgroundService
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

                    var sessions = await dbContext.Sessions
                        .Where(s => s.StartDate > DateTime.Now && !s.IsDeleted)
                        .ToListAsync(cancellationToken);

                    var sessionTasks = sessions.Select(async session =>
                    {
                        try
                        {
                            var reservations = await dbContext.Reservations
                                .Where(r => r.SessionId == session.SessionId && !r.IsDeleted)
                                .ToListAsync(cancellationToken);

                            var reservationTasks = reservations.Select(async reservation =>
                            {
                                if (reservation.TokenCreateDate < DateTime.Now.AddMinutes(-30))
                                {
                                    reservation.Token = Guid.NewGuid().ToString();
                                    reservation.TokenCreateDate = DateTime.Now;

                                    dbContext.Reservations.Update(reservation);
                                }
                            });

                            await Task.WhenAll(reservationTasks);

                            await dbContext.SaveChangesAsync(cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Session {session.SessionId} error: {ex.Message}");
                        }
                    });

                    await Task.WhenAll(sessionTasks);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Reservation token background service error: {ex}");
                }

                await Task.Delay(TimeSpan.FromMinutes(30), cancellationToken);
            }
        }
    }
}
