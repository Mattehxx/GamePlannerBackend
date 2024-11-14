using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers.IManagers;

namespace GamePlanner.DAL.Managers
{
    public class PreferenceManager(GamePlannerDbContext context) : GenericManager<Preference>(context), IPreferenceManager
    {
        public override async Task<Preference> DeleteAsync(int id)
        {
            Preference entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0
                ? entity
                : throw new InvalidOperationException("Failed to delete entity");
        }
    }
}
