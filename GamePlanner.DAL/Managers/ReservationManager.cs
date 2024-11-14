using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers.IManagers;
using GamePlanner.Services;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.DAL.Managers
{
    public class ReservationManager(GamePlannerDbContext context) : GenericManager<Reservation>(context), IReservationManager
    {
        /// <summary>
        /// Logical delete reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public override async Task<Reservation> DeleteAsync(int id)
        {
            Reservation entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0
                ? entity
                : throw new InvalidOperationException("Failed to delete entity");
        }

        /// <summary>
        /// Get reservation by session and user
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Reservation> GetBySessionAndUser(int sessionId, string userId)
        {
            return await _dbSet.SingleAsync(r => r.SessionId == sessionId && r.UserId == userId)
                ?? throw new InvalidOperationException("Reservation not found");
        }

        /// <summary>
        /// Confirm reservation
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Reservation> ConfirmAsync(Reservation entity, string token)
        {
            if (entity.Token != token) throw new InvalidOperationException("Invalid token");

            entity.IsConfirmed = true;
            return await _context.SaveChangesAsync() > 0
                ? entity
                : throw new InvalidOperationException("Failed to confirm reservation");
        }

        /// <summary>
        /// Confirm notification
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Reservation> ConfirmNotificationAsync(Reservation entity)
        {
            entity.IsNotified = true;
            return await _context.SaveChangesAsync() > 0
                ? entity
                : throw new InvalidOperationException("Failed to confirm notification");
        }

        public async Task<IEnumerable<Reservation>> GetConfirmedAsync(int sessionId)
        {
            return await _context.Reservations
                .Where(r => r.SessionId == sessionId && !r.IsDeleted && r.IsConfirmed)
                .ToListAsync();
        }

        public async Task<Reservation> GetFirstQueuedAsync(int sessionId)
        {
            return await _context.Reservations
                .Where(r => r.SessionId == sessionId && !r.IsDeleted && !r.IsConfirmed && !r.IsNotified)
                .OrderBy(r => r.ReservationId)
                .FirstAsync();
        }
    }
}
