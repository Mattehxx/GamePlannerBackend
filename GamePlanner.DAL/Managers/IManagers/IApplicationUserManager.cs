using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;
using Microsoft.AspNetCore.JsonPatch;

namespace GamePlanner.DAL.Managers.IManagers
{
    public interface IApplicationUserManager : IManager<ApplicationUser>
    {
        public Task<ApplicationUser> DisableOrEnableUser(string userId);
        public Task<ApplicationUser> DeleteAsync(string id);
        public Task<ApplicationUser> PatchAsync(string id, JsonPatchDocument<ApplicationUser> patchDocument);
    }
}
