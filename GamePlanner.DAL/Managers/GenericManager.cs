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

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual IQueryable Get(ODataQueryOptions<T> oDataQueryOptions)
        {
            return oDataQueryOptions.ApplyTo(_dbSet);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id)
                ?? throw new InvalidOperationException("Entity not found");
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0 ? entity
                : throw new InvalidOperationException("Failed to create entity");
        }

        public async Task<T> UpdateAsync(int id, JsonPatchDocument<T> patchDocument)
        {
            var entity = await _dbSet.FindAsync(id) 
                ?? throw new InvalidOperationException("Entity not found");

            patchDocument.ApplyTo(entity);
            return await _context.SaveChangesAsync() > 0 ? entity
               : throw new InvalidOperationException("Failed to update entity");
        }

        public virtual async Task<T> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id)
                ?? throw new InvalidOperationException("Entity not found");

            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0 ? entity
                : throw new InvalidOperationException("Failed to delete entity");
        }
    }
}
