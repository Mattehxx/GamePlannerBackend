using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;
using GamePlanner.DTO.InputDTO;
using GamePlanner.DTO.OutputDTO;
using GamePlanner.DTO.OutputDTO.DetailDTO;
using GamePlanner.DTO.OutputDTO.GeneralDTO;

namespace GamePlanner.DTO
{
    public class Mapper
    {
        #region ToEntity
        public Event ToEntity(EventInputDTO model) => new Event
        {
            EventId = 0,
            AdminId = model.AdminId,
            IsDeleted = model.IsDeleted,
            Description = model.Description,
            ImgUrl = model.ImgUrl,
            IsPublic = model.IsPublic,
            Name = model.Name
        };
        public Game ToEntity(GameInputDTO model) => new Game
        {
            GameId = 0,
            IsDeleted = model.IsDeleted,
            IsDisabled = model.IsDisabled,
            Description = model.Description,
            ImgUrl = model.ImgUrl,
            Name = model.Name,
        };
        public Knowledge ToEntity(KnowledgeInputDTO model) => throw new NotImplementedException();
        public Preference ToEntity(PreferenceInputDTO model) => throw new NotImplementedException();
        public Reservation ToEntity(ReservationInputDTO model) => throw new NotImplementedException();
        public Session ToEntity(SessionInputDTO model) => throw new NotImplementedException();
        #endregion


        #region ToModel
        public EventOutputDTO ToModel(Event entity) => new EventOutputDTO
        {
           EventId = entity.EventId,
           ImgUrl = entity.ImgUrl,
           IsDeleted = entity.IsDeleted,
           IsPublic = entity.IsPublic,
           Name = entity.Name
        };
        public GameOutputDTO ToModel(Game model) => throw new NotImplementedException();
        public KnowledgeOutputDTO ToModel(Knowledge model) => throw new NotImplementedException();
        public PreferenceOutputDTO ToModel(Preference model) => throw new NotImplementedException();
        public ReservationOutputDTO ToModel(Reservation model) => throw new NotImplementedException();
        public SessionOutputDTO ToModel(Session model) => throw new NotImplementedException();



        #endregion

        #region Detailed model
        public EventDetailsDTO ToDetailModel(Event model) => throw new NotImplementedException();
        public GameDetailsDTO ToDetailModel(Game model) => throw new NotImplementedException();
        public KnowledgeDetailsDTO ToDetailModel(Knowledge model) => throw new NotImplementedException();
        public SessionDetailsDTO ToDetailModel(Session model) => throw new NotImplementedException();

        #endregion

    }
}
