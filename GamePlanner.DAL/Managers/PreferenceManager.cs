using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers.IManagers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

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
        public override IQueryable Get(ODataQueryOptions<Preference> oDataQueryOptions)
        {
            return oDataQueryOptions.ApplyTo(_dbSet.Include(p => p.User).Include(p => p.Game).Include(p => p.Knowledge));
        }
    }
}
