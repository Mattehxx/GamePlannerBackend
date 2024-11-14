using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers.IManagers;

namespace GamePlanner.DAL.Managers
{
    public class SessionManager(GamePlannerDbContext context) : GenericManager<Session>(context), ISessionManager
    {
        public override async Task<Session> DeleteAsync(int id)
        {
            Session entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0
                ? entity
                : throw new InvalidOperationException("Failed to delete entity");
        }
    }
}
