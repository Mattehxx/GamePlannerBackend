using GamePlanner.DAL.Data.Entity;

namespace GamePlanner.DAL.Managers.IManagers
{
    public interface IReservationManager : IManager<Reservation>
    {
        public Task<Reservation> ConfirmAsync(int sessionId, string userId, string token);
    }
}
