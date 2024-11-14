using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers.IManagers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.DAL.Managers
{
    public class GameManager(GamePlannerDbContext context) : GenericManager<Game>(context), IGameManager
    {
        public override async Task<Game> DeleteAsync(int id)
        {
            Game entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0
                ? entity
                : throw new InvalidOperationException("Failed to delete entity");
        }
        public override IQueryable Get(ODataQueryOptions<Game> oDataQueryOptions)
        {
            return oDataQueryOptions.ApplyTo(_dbSet.Include(g => g.Sessions).Include(g => g.Preferences));
        }

        public async Task<Game> DisableGame(int gameId ,bool isDisable)
        {
            Game entity = await GetByIdAsync(gameId);
            entity.IsDisabled = isDisable;
            return await _context.SaveChangesAsync() > 0
                ? entity
                : throw new InvalidOperationException("Failed to disable entity");
        }
    }
}
