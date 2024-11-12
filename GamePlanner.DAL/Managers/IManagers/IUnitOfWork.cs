using GamePlanner.DAL.Managers.IManagers;
using Microsoft.AspNetCore.Mvc;

namespace GamePlanner.Services
{
    public interface IUnitOfWork
    {
        public IEventManager EventManager { get; }
        public IGameSessionManager GameSessionManager { get; }
        public IGameManager GameManager { get; }
        public ITableManager TableManager { get; }
        public IKnowledgeManager KnowledgeManager { get; }
        public IRecurrenceManager RecurrenceManager { get; }
        public IReservationManager ReservationManager { get; }
        public Task<ActionResult<bool>> Commit();
    }
}
