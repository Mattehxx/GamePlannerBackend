using GamePlanner.DAL.Data.Entity;

namespace GamePlanner.DAL.Managers.IManagers
{
    public interface IReservationManager : IManager<Reservation>
    {
        public Task<Reservation> GetBySessionAndUser(int sessionId, string userId);
        public Task<Reservation> ConfirmAsync(Reservation entity, string token);
        public Task<Reservation> ToggleNotificationAsync(Reservation entity, bool isNotified);
        public Task<IEnumerable<Reservation>> GetConfirmedAsync(int sessionId);
        public Task<Reservation?> GetFirstQueuedAsync(int sessionId);
    }
}
