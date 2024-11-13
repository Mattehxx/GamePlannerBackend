using GamePlanner.DAL.Data;
using GamePlanner.DAL.Managers.IManagers;
using GamePlanner.Managers;
using GamePlanner.Services;

namespace GamePlanner.DAL.Managers
{
    public class UnitOfWork(GamePlannerDbContext context) : IUnitOfWork
    {
        public readonly GamePlannerDbContext _context = context;
        public IEventManager EventManager { get; private set; } = new EventManager(context);
        public IGameManager GameManager { get; private set; } = new GameManager(context);
        public IKnowledgeManager KnowledgeManager { get; private set; } = new KnowledgeManager(context);
        public IPreferenceManager PreferenceManager { get; private set; } = new PreferenceManager(context);
        public IReservationManager ReservationManager { get; private set; } = new ReservationManager(context);
        public ISessionManager SessionManager { get; private set; } = new SessionManager(context);
    }
}
