using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.OData.Query;

namespace GamePlanner.DAL.Managers.IManagers
{
    public interface IManager<T> where T : class
    {
        public IQueryable Get(ODataQueryOptions<T> oDataQueryOptions);
        public Task<T> GetByIdAsync(int id);
        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(int id, JsonPatchDocument<T> patchDocument);
        public Task<T> DeleteAsync(int id);
    }
}
