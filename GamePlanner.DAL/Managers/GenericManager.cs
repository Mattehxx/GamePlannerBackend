using GamePlanner.DAL.Data;
using GamePlanner.DAL.Managers.IManagers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
namespace GamePlanner.DAL.Managers
{
    public class GenericManager<T> : IManager<T> where T : class
    {
        protected readonly GamePlannerDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericManager(GamePlannerDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAsync(ODataQueryOptions oDataQueryOptions)
        {
            var query = (IQueryable<T>)oDataQueryOptions.ApplyTo(_dbSet);
            return await query.ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
           _dbSet.Add(entity);
           await _context.SaveChangesAsync();
           return entity;
        }

        public async Task<T> UpdateAsync(int id, JsonPatchDocument<T> patchDocument)
        {
            var entity = await _dbSet.FindAsync(id);
            patchDocument.ApplyTo(entity);
            return entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            return entity;
        }
    }
}
