using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Db;
using GamePlanner.DAL.Managers.IManagers;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.DAL.Managers
{
    public class TableManager(GamePlannerDbContext context) : GenericManager<Table>(context), ITableManager
    {
        public async Task<IEnumerable<object>> GetTablesAvailability(DateTime start, DateTime end)
        {
            var gameSessions = _context.GameSessions.Where(gs => gs.GameSessionStartDate >= start 
            && gs.GameSessionStartDate <= end 
            || gs.GameSessionEndDate >= start && gs.GameSessionEndDate <= end);
            return await _dbSet.Select(t => new
            {
                t.TableId,
                t.Name,
                t.Seat,
                IsAvailable = !gameSessions.Any(gs => gs.TableId == t.TableId) && !t.IsDeleted
            }).ToListAsync();
        }
    }}
