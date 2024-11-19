using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers.IManagers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

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
        public override IQueryable Get(ODataQueryOptions<Session> oDataQueryOptions)
        {
            return oDataQueryOptions.ApplyTo(_dbSet.Where(set => !set.IsDeleted));
        }

        public IQueryable<Session> GetUpcomingSessions()
        {
            return _dbSet.Include(s=>s.Master).Include(s=>s.Event).OrderBy(s => s.StartDate).Take(10)
                ?? throw new Exception("no sessions found");
        }
    }
}
