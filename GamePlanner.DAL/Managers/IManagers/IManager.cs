using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.OData.Query;

namespace GamePlanner.DAL.Managers.IManagers
{
    public interface IManager<T> where T : class
    {
        public Task<List<T>> GetAsync(ODataQueryOptions oDataQueryOptions);
        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(int id,JsonPatchDocument<T> patchDoc);
        public Task<T> DeleteAsync(int id);
    }
}
