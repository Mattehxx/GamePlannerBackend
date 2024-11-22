using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers.IManagers;
using Microsoft.AspNetCore.OData.Query;
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
            return await _dbSet.SingleAsync(r => r.SessionId == sessionId && r.UserId == userId && !r.IsDeleted)
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
            _context.Update(entity);
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
        public async Task<Reservation> ToggleNotificationAsync(Reservation entity, bool isNotified)
        {
            entity.IsNotified = isNotified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Reservation>> GetConfirmedAsync(int sessionId)
        {
            return await _context.Reservations
                .Where(r => r.SessionId == sessionId && !r.IsDeleted && r.IsConfirmed)
                .ToListAsync();
        }

        public async Task<Reservation?> GetFirstQueuedAsync(int sessionId)
        {
            return await _context.Reservations
                .Where(r => r.SessionId == sessionId && !r.IsDeleted && !r.IsConfirmed && !r.IsNotified)
                .OrderBy(r => r.ReservationId)
                .FirstOrDefaultAsync();
        }
        public override IQueryable Get(ODataQueryOptions<Reservation> oDataQueryOptions)
        {
            return oDataQueryOptions.ApplyTo(_dbSet.Where(set => !set.IsDeleted));
        }

        public override Task<Reservation> CreateAsync(Reservation entity)
        {
            if (_dbSet.Any(r => r.SessionId == entity.SessionId && r.UserId == entity.UserId && !r.IsDeleted)) 
                throw new InvalidOperationException("Reservation already exists");
            return base.CreateAsync(entity);
        }
    }
}
