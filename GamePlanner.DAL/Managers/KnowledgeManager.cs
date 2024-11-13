using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Data;
using GamePlanner.DAL.Managers.IManagers;

namespace GamePlanner.DAL.Managers
{
    public class KnowledgeManager(GamePlannerDbContext context) : GenericManager<Knowledge>(context), IKnowledgeManager
    {
        public override async Task<Knowledge> DeleteAsync(int id)
        {
            Knowledge entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0
                ? entity
                : throw new InvalidOperationException("Failed to delete entity");
        }
    }
}
