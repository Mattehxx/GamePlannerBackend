using GamePlanner.DAL.Managers.IManagers;
using Microsoft.AspNetCore.Mvc;

namespace GamePlanner.Services
{
    public interface IUnitOfWork
    {
        public IEventManager EventManager { get; }
        public IGameManager GameManager { get; }
        public IKnowledgeManager KnowledgeManager { get; }
        public IPreferenceManager PreferenceManager { get; }
        public IReservationManager ReservationManager { get; }
        public ISessionManager SessionManager { get; }
    }
}
