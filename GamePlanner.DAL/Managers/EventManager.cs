using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Db;
using GamePlanner.DAL.Managers;
using GamePlanner.DAL.Managers.IManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace GamePlanner.Managers
{
    public class EventManager(GamePlannerDbContext context) : GenericManager<Event>(context), IEventManager
    {
        public override async Task<Event> CreateAsync(Event entity)
        {
            entity.Recurrence.Name = "settimanale";
            var delayDay = 7;
            var startDate = entity.EventStartDate;
            while(startDate < entity.EventEndDate)
            {
                var endDate = startDate.AddHours(entity.Duration);
                int tableId = await _context.Tables
                    .Include(t => t.GameSessions)
                    .Where(t => !t.IsDeleted && !t.GameSessions
                    .Any(gs => (gs.GameSessionStartDate >= startDate && gs.GameSessionStartDate <= endDate)
                        || (gs.GameSessionEndDate >= startDate && gs.GameSessionEndDate <= endDate)))
                    .Select(gs => gs.TableId)
                    .FirstOrDefaultAsync();

                if (tableId != 0)
                {
                    entity.GameSessions?.Add(new GameSession
                    {
                        GameSessionId = 0,
                        GameSessionStartDate = startDate,
                        GameSessionEndDate = endDate,
                        IsDelete = false,
                        EventId = entity.EventId,
                        TableId = tableId
                    });
                }

                startDate.AddDays(delayDay);
            }

            entity.GameSessions.AddRange(
                new List<GameSession>()
                {
                    
                });
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
