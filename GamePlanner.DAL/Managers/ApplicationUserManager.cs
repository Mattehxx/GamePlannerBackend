using GamePlanner.DAL.Data;
using GamePlanner.DAL.Managers.IManagers;
using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;

namespace GamePlanner.DAL.Managers
{
    public class ApplicationUserManager(GamePlannerDbContext context) : GenericManager<ApplicationUser>(context), IApplicationUserManager
    {
        public override async Task<ApplicationUser> DeleteAsync(int id)
        {
            ApplicationUser entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0
                ? entity
                : throw new InvalidOperationException("Failed to delete entity");
        }
    }
}
