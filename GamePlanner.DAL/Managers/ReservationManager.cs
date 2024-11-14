using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers.IManagers;

namespace GamePlanner.DAL.Managers
{
    public class ReservationManager(GamePlannerDbContext context) : GenericManager<Reservation>(context), IReservationManager
    {
        public override async Task<Reservation> DeleteAsync(int id)
        {
            Reservation entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0
                ? entity
                : throw new InvalidOperationException("Failed to delete entity");
        }
    }
}
