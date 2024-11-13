using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Db;
using GamePlanner.DTO.InputDTO;
using GamePlanner.DTO.OutputDTO.DetailDTO;
using GamePlanner.DTO.OutputDTO.GeneralDTO;

namespace GamePlanner.DTO
{
    public class Mapper
    {
        #region ToEntity
        public Event ToEntity(EventInputDTO model) => throw new NotImplementedException();
        
        public Game ToEntity(GameInputDTO model) => throw new NotImplementedException();
        public GameSession ToEntity(GameSessionInputDTO model) => throw new NotImplementedException();
        public Recurrence ToEntity(RecurrenceInputDTO model) => throw new NotImplementedException();
        public Reservation ToEntity(ReservationInputDTO model) => throw new NotImplementedException();
        public Table ToEntity(TableInputDTO model) => throw new NotImplementedException();
        #endregion


        #region ToModel
        public EventOutputDTO ToModel(Event entity) => throw new NotImplementedException();
        public GameOutputDTO ToModel(Game entity) => throw new NotImplementedException();
        public GameSessionOutputDTO ToModel(GameSession entity) => throw new NotImplementedException();
        public RecurrenceOutputDTO ToModel(Recurrence entity) => throw new NotImplementedException();
        public ReservationOutputDTO ToModel(Reservation entity) => throw new NotImplementedException();
        public TableOutputDTO ToModel(Table entity) => throw new NotImplementedException();
        public UserOutputDTO ToModel(ApplicationUser entity) => throw new NotImplementedException();
        #endregion

        #region Detailed model
        public EventDetailsDTO ToDetailedModel(Event entity) => throw new NotImplementedException();
        public GameSessionDetailsDTO ToDetailedModel(GameSession entity) => throw new NotImplementedException();
        #endregion
    }
}
