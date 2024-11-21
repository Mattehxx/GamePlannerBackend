using GamePlanner.DAL.Data;
using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Managers.IManagers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace GamePlanner.DAL.Managers
{
    public class ApplicationUserManager(GamePlannerDbContext context) : GenericManager<ApplicationUser>(context), IApplicationUserManager
    {
        public async Task<ApplicationUser> PatchAsync(string id, JsonPatchDocument<ApplicationUser> patchDocument)
        {
            var entity = await _dbSet
                .Include(u => u.Preferences)
                    .ThenInclude(p => p.Game)
                .Include(u => u.Preferences)
                    .ThenInclude(p => p.Knowledge)
                .SingleOrDefaultAsync(u => u.Id == id)
                ?? throw new InvalidOperationException("Entity not found");

            patchDocument.ApplyTo(entity);
            return await _context.SaveChangesAsync() > 0 ? entity
               : throw new InvalidOperationException("Failed to update entity");
        }

        public async Task<ApplicationUser> DeleteAsync(string id)
        {
            ApplicationUser entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0
                ? entity
                : throw new InvalidOperationException("Failed to delete entity");
        }
        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _dbSet.SingleOrDefaultAsync(user=>user.Id == id) 
                ?? throw new InvalidOperationException("user not found");
        }

        public async Task<ApplicationUser> DisableOrEnableUser(string userId)
        {
            ApplicationUser entity = await GetByIdAsync(userId);
            entity.IsDisabled = !entity.IsDisabled;
            return await _context.SaveChangesAsync() > 0
                ? entity
                : throw new InvalidOperationException("Failed to disable entity");
        }
        
        public override IQueryable Get(ODataQueryOptions<ApplicationUser> oDataQueryOptions)
        {
            return oDataQueryOptions.ApplyTo(_dbSet.Where(set => !set.IsDeleted));
        }
    }
}
