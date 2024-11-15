using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;

namespace GamePlanner.DAL.Managers.IManagers
{
    public interface IApplicationUserManager : IManager<ApplicationUser>
    {
        public Task<ApplicationUser> DisableOrEnableUser(string userId);
    }
}
