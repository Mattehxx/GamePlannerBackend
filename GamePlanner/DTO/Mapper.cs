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
        public Event ToEntity(EventInputDTO model) => new Event
        {
            EventId = 0,
            AdminId = model.AdminId,
            GameId = model.GameId,
            RecurrenceId = model.RecurrenceId,
            IsDeleted = model.IsDeleted,
            Description = model.Description,
            Duration = model.Duration,
            EventStartDate = model.EventStartDate,
            EventEndDate = model.EventEndDate.AddDays(model.Duration),
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
            Name = model.Name
        };
        public GameSession ToEntity(GameSessionInputDTO model) => new GameSession
        {
            GameSessionId = 0,
            EventId = model.EventId,
            MasterId = model.MasterId,
            TableId = model.TableId,
            IsDelete = model.IsDeleted,
            GameSessionStartDate = model.GameSessionStartDate,
            GameSessionEndDate = model.GameSessionEndDate,
        };
        public Recurrence ToEntity(RecurrenceInputDTO model) => new Recurrence
        {
            RecurrenceId = 0,
            Name = model.Name,
            Day = model.Day
        };
        public Reservation ToEntity(ReservationInputDTO model) => new Reservation
        {
            ReservationId = 0,
            GameSessionId = model.GameSessionId,
            UserId = model.UserId,
            IsDeleted = model.IsDelete,
            Surname = model.Surname,
            Email = model.Email,
            IsConfirmed = model.IsConfirmed,
            IsQueued = model.IsQueued,
            Name = model.Name,
            Phone = model.Phone,
            BirthDate = model.BirthDate,
        };
        public Table ToEntity(TableInputDTO model) => new Table
        {
            TableId = 0,
            IsDeleted = model.IsDeleted,
            Name = model.Name,
            Seat = model.Seat,
        };
        #endregion


        #region ToModel
        public EventOutputDTO ToModel(Event entity) => new EventOutputDTO
        {
            EventId = entity.EventId,
            GameId = entity.GameId,
            RecurrenceId = entity.RecurrenceId,
            IsDeleted = entity.IsDeleted,
            Duration = entity.Duration,
            EventStartDate = entity.EventStartDate,
            EventEndDate = entity.EventStartDate.AddDays(entity.Duration),
            ImgUrl = entity.ImgUrl,
            IsPublic = entity.IsPublic,
            Name = entity.Name
        };
        public GameOutputDTO ToModel(Game entity) => new GameOutputDTO
        {
            GameId = entity.GameId,
            IsDeleted = entity.IsDeleted,
            IsDisabled = entity.IsDisabled,
            Description = entity.Description,
            ImgUrl = entity.ImgUrl,
            Name = entity.Name,
            
        };
        public GameSessionOutputDTO ToModel(GameSession entity) => new GameSessionOutputDTO
        {
            GameSessionId = entity.GameSessionId,
            EventId = entity.EventId,
            TableId = entity.TableId,
            IsDelete = entity.IsDelete,
            GameSessionStartDate = entity.GameSessionStartDate,
            GameSessionEndDate = entity.GameSessionEndDate
        };
        public RecurrenceOutputDTO ToModel(Recurrence entity) => new RecurrenceOutputDTO
        {
            RecurrenceId = entity.RecurrenceId,
            Name = entity.Name,
        };
        public ReservationOutputDTO ToModel(Reservation entity) => new ReservationOutputDTO
        {
            ReservationId = entity.ReservationId,
            UserId = entity.UserId,
            IsDelete = entity.IsDeleted,
            IsConfirmed = entity.IsConfirmed,
            IsQueued = entity.IsQueued,
            GameSessionId = entity.GameSessionId,
            Email = entity.Email,
            Surname = entity.Surname,
            Name = entity.Name,
            Phone = entity.Phone
        };
        public TableOutputDTO ToModel(Table entity) => new TableOutputDTO
        {
            TableId = 0,
            IsDeleted = entity.IsDeleted,
            Name = entity.Name,
            Seat = entity.Seat,
        };
        public UserOutputDTO ToModel(ApplicationUser entity) => new UserOutputDTO
        {
            Id = entity.Id,
            ImgUrl = entity.ImgUrl,
            CanBeMaster = entity.CanBeMaster,
            Email = entity.Email,
            Level = entity.Level,
            Name = entity.Name,
            Surname = entity.Surname
        };
        #endregion

        #region Detailed model
        public EventDetailsDTO ToDetailedModel(Event entity) => new EventDetailsDTO
        {
            EventId = entity.EventId,
            EventStartDate = entity.EventStartDate,
            EventEndDate = entity.EventEndDate,
            EventDescription = entity.Description,
            IsPublic = entity.IsPublic,
            Name = entity.Name,
            Duration = entity.Duration,
            GameId = entity.GameId,
            ImgUrl = entity.ImgUrl,
            IsDeleted = entity.IsDeleted,
            RecurrenceId = entity.RecurrenceId,
            GameSessionsGeneral = entity.GameSessions?.ConvertAll(ToModel)
        };
        public GameSessionDetailsDTO ToDetailedModel(GameSession entity) => new GameSessionDetailsDTO
        {
            GameSessionId = entity.GameSessionId,
            EventId = entity.EventId,
            MasterId = entity.MasterId,
            TableId = entity.TableId,
            IsDelete = entity.IsDelete,
            GameSessionStartDate = entity.GameSessionStartDate,
            GameSessionEndDate  = entity.GameSessionEndDate,
            TotalSeats = entity.Table != null ? entity.Table.Seat : 0,
            QueueLength = entity.Reservations != null ? entity.Reservations.Count(r=>r.IsQueued) : 0,
            AvailableSeats = entity.Reservations != null && entity.Table != null 
            ? entity.Table.Seat - entity.Reservations.Count() 
            : entity.Table != null ? entity.Table.Seat : 0,
            Reservations = entity.Reservations?.ConvertAll(ToModel),
            
        };
        #endregion
    }
}
