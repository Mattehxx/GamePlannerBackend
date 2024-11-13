using GamePlanner.DAL.Data;
using GamePlanner.DAL.Managers.IManagers;
using GamePlanner.Managers;
using GamePlanner.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamePlanner.DAL.Managers
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly GamePlannerDbContext _context;
        public UnitOfWork(GamePlannerDbContext context)
        {
            _context = context;
            EventManager = new EventManager(context);
            GameManager = new GameManager(context);
            GameSessionManager = new GameSessionManager(context);
            RecurrenceManager = new RecurrenceManager(context);
            ReservationManager = new ReservationManager(context);
            TableManager = new TableManager(context);
        }

        public IEventManager EventManager { get; private set; }
        public IGameManager GameManager { get; private set; }
        public IGameSessionManager GameSessionManager { get; private set; }
        public IRecurrenceManager RecurrenceManager { get; private set; }
        public IReservationManager ReservationManager { get; private set; }
        public ITableManager TableManager { get; private set; }

        public async Task<IActionResult<bool>> Commit()
        {
            return  await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
