using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers;
using GamePlanner.DAL.Managers.IManagers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.Managers
{
    public class EventManager(GamePlannerDbContext context) : GenericManager<Event>(context), IEventManager
    {
        public override async Task<Event> DeleteAsync(int id)
        {
            Event entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0 
                ? entity
                : throw new InvalidOperationException("Failed to delete entity");
        }
        public override IQueryable Get(ODataQueryOptions<Event> oDataQueryOptions)
        {
            return oDataQueryOptions.ApplyTo(_dbSet.Include(e => e.AdminUser).Include(e => e.Sessions));
        }
    }
}
