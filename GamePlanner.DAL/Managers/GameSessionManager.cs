using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Db;
using GamePlanner.DAL.Managers.IManagers;

namespace GamePlanner.DAL.Managers
{
    public class GameSessionManager(GamePlannerDbContext context) : GenericManager<GameSession>(context), IGameSessionManager
    {
    }
}
