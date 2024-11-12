using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Db;
using GamePlanner.DAL.Managers.IManagers;

namespace GamePlanner.DAL.Managers
{
    public class KnowledgeManager(GamePlannerDbContext context) : GenericManager<Knowledge>(context), IKnowledgeManager
    {
    }
}
