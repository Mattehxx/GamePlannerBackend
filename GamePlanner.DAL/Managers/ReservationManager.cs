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
        /// Confirm reservation
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Reservation> ConfirmAsync(int sessionId, string userId, string token)
        {
            Reservation entity = _context.Reservations.Single(r => r.SessionId == sessionId && r.UserId == userId && r.Token == token);
            entity.IsConfirmed = true;
            return await _context.SaveChangesAsync() > 0
                ? entity
                : throw new InvalidOperationException("Failed to confirm reservation");
        }
        public override IQueryable Get(ODataQueryOptions<Reservation> oDataQueryOptions)
        {
            return oDataQueryOptions.ApplyTo(_dbSet.Include(r => r.User).Include(r => r.Session));
        }
    }
}
