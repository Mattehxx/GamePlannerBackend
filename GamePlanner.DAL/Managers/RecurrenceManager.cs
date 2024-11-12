using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Db;
using GamePlanner.DAL.Managers.IManagers;

namespace GamePlanner.DAL.Managers
{
    public class RecurrenceManager(GamePlannerDbContext context) : GenericManager<Recurrence>(context), IRecurrenceManager
    {
    }
}
