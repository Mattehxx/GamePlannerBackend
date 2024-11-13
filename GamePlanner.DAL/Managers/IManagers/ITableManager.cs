using GamePlanner.DAL.Data.Db;

namespace GamePlanner.DAL.Managers.IManagers
{
    public interface ITableManager : IManager<Table>
    {
        public Task<IEnumerable<object>> GetTablesAvailability(DateTime start, DateTime end);
    }
}
