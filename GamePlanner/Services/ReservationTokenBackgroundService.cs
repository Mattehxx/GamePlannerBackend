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

                    foreach (var session in sessions) 
                    {
                        var reservations = await dbContext.Reservations
                                .Where(r => r.SessionId == session.SessionId && !r.IsDeleted)
                                .ToListAsync(cancellationToken);

                        foreach (var reservation in reservations)
                        {
                            if (reservation.TokenCreateDate < DateTime.Now.AddMinutes(-30))
                            {
                                reservation.Token = Guid.NewGuid().ToString();
                                reservation.TokenCreateDate = DateTime.Now;

                                dbContext.Reservations.Update(reservation);
                            }
                        }

                        await dbContext.SaveChangesAsync(cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Reservation token background service error: {ex}");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }
    }
}
