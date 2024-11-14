using GamePlanner.DAL.Data.Entity;

namespace GamePlanner.DAL.Managers.IManagers
{
    public interface IEventManager : IManager<Event>
    {
       public Task<bool> CreateNewRecurrenceSessions(int eventId,DateTime newDate);
    }
}
