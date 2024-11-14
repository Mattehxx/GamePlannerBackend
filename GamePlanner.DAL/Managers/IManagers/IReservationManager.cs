using GamePlanner.DAL.Data.Entity;

namespace GamePlanner.DAL.Managers.IManagers
{
    public interface IReservationManager : IManager<Reservation>
    {
        public Task<Reservation> GetBySessionAndUser(int sessionId, string userId);
        public Task<Reservation> ConfirmAsync(Reservation entity, string token);
    }
}
