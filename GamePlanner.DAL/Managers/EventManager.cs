using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Db;
using GamePlanner.DAL.Managers;
using GamePlanner.DAL.Managers.IManagers;

namespace GamePlanner.Managers
{
    public class EventManager(GamePlannerDbContext context) : GenericManager<Event>(context), IEventManager
    {
        
    }
}
