using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers.IManagers;

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
    }
}
