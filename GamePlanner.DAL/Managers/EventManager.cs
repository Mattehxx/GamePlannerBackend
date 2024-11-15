using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DAL.Managers;
using GamePlanner.DAL.Managers.IManagers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.Managers
{
    public class EventManager(GamePlannerDbContext context) : GenericManager<Event>(context), IEventManager
    {
        public async Task<bool> CreateNewRecurrenceSessions(int eventId, DateTime newDate)
        {
            var myEvent = _dbSet.AsQueryable().Include(e => e.Sessions).SingleOrDefault(e => e.EventId.Equals(eventId))
                ?? throw new InvalidOperationException($"event with id:{eventId} not found");
            DateTime lastDate = myEvent.Sessions?.Last().StartDate ?? throw new Exception("sessions not found");
            var sessions = myEvent.Sessions?.Where(s => s.StartDate.Date == lastDate.Date) ?? throw new InvalidOperationException();
            foreach (var session in sessions)
            {
                var start = new DateTime(
                    newDate.Year,
                    newDate.Month,
                    newDate.Day,
                    session.StartDate.Hour,
                    session.StartDate.Minute,
                    session.StartDate.Second
                );
                var end = start.AddHours((session.EndDate - session.StartDate).TotalHours);
                myEvent.Sessions.Add(new Session
                {
                    SessionId = 0,
                    EventId = eventId,
                    StartDate = start,
                    EndDate = end,
                    GameId = session.GameId,
                    Seats = session.Seats,
                    IsDeleted = session.IsDeleted,
                    MasterId = session.MasterId
                });
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public override async Task<Event> DeleteAsync(int id)
        {
            Event entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0
                ? entity
                : throw new InvalidOperationException("Failed to delete entity");
        }
        public override IQueryable Get(ODataQueryOptions<Event> oDataQueryOptions)
        {
            return oDataQueryOptions.ApplyTo(_dbSet.Include(e => e.AdminUser).Include(e => e.Sessions));
        }
    }
}
