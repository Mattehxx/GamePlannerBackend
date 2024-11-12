using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Db;
using GamePlanner.DTO.InputDTO;
using GamePlanner.DTO.OutputDTO;

namespace GamePlanner.DTO
{
    public class Mapper
    {
        #region ToEntity
        public Event ToEntity(EventInputDTO model) => new Event
        {
            AdminId = model.AdminId,
            EventId = 0,
            GameId = model.GameId,
            RecurrenceId = model.RecurrenceId,
            IsDeleted = model.IsDeleted,
            Description = model.Description,
            Duration = model.Duration,
            EventDate = model.EventDate,
            EventEndDate = model.EventDate.AddDays(model.Duration),
            ImgUrl = model.ImgUrl,
            IsPublic = model.IsPublic,
            Name = model.Name,
            Game = model.Game != null ? ToEntity(model.Game) : null,
            Recurrence = model.Recurrence != null ? ToEntity(model.Recurrence) : null,
            //user
            GameSessions = model.GameSessions?.ConvertAll(ToEntity)
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
        public GameSession ToEntity(GameSessionInputDTO model) => new GameSession
        {
            GameSessionId = 0,
            EventId = model.EventId,
            MasterId = model.MasterId,
            TableId = model.TableId,
            IsDeleted = model.IsDelete,
            GameSessionDate = model.GameSessionDate,
            GameSessionEndTime = model.GameSessionEndTime,
        };
        public Recurrence ToEntity(RecurrenceInputDTO model) => new Recurrence
        {
            RecurrenceId = 0,
            Name = model.Name,
            Events = model.Events?.ConvertAll(ToEntity),
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
            GameSession = model.GameSession != null ? ToEntity(model.GameSession) : null,
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
            AdminId = entity.AdminId,
            EventId = 0,
            GameId = entity.GameId,
            RecurrenceId = entity.RecurrenceId,
            IsDeleted = entity.IsDeleted,
            Description = entity.Description,
            Duration = entity.Duration,
            EventDate = entity.EventDate,
            EventEndDate = entity.EventDate.AddDays(entity.Duration),
            ImgUrl = entity.ImgUrl,
            IsPublic = entity.IsPublic,
            Name = entity.Name,
            Game = entity.Game != null ? ToModel(entity.Game) : null,
            Recurrence = entity.Recurrence != null ? ToModel(entity.Recurrence) : null,
            //user
            GameSessions = entity.GameSessions?.ConvertAll(ToModel),
            User = entity.User != null ? ToModel(entity.User) : null,
        };
        public GameOutputDTO ToModel(Game entity) => new GameOutputDTO
        {
            GameId = 0,
            IsDeleted = entity.IsDeleted,
            IsDisabled = entity.IsDisabled,
            Description = entity.Description,
            ImgUrl = entity.ImgUrl,
            Name = entity.Name,
        };
        public GameSessionOutputDTO ToModel(GameSession entity) => new GameSessionOutputDTO
        {
            GameSessionId = 0,
            EventId = entity.EventId,
            MasterId = entity.MasterId,
            TableId = entity.TableId,
            IsDelete = entity.IsDeleted,
            GameSessionDate = entity.GameSessionDate,
            GameSessionEndTime = entity.GameSessionEndTime,
        };
        public RecurrenceOutputDTO ToModel(Recurrence entity) => new RecurrenceOutputDTO
        {
            RecurrenceId = 0,
            Name = entity.Name,
            //Events = entity.Events?.ConvertAll(ToModel),
        };
        public ReservationOutputDTO ToModel(Reservation entity) => new ReservationOutputDTO
        {
            ReservationId = entity.ReservationId,
            GameSessionId = entity.GameSessionId,
            UserId = entity.UserId,
            IsDelete = entity.IsDeleted,
            Surname = entity.Surname,
            Email = entity.Email,
            IsConfirmed = entity.IsConfirmed,
            IsQueued = entity.IsQueued,
            Name = entity.Name,
            Phone = entity.Phone,
            BirthDate = entity.BirthDate,
            GameSession = entity.GameSession != null ? ToModel(entity.GameSession) : null,
        };
        public TableOutputDTO ToModel(Table entity) => new TableOutputDTO
        {
            TableId = 0,
            IsDeleted = entity.IsDeleted,
            Name = entity.Name,
            Seat = entity.Seat,
        };
        public ApplicationUserOutputDTO ToModel(ApplicationUser entity) => new ApplicationUserOutputDTO
        {
            Id = entity.Id,
            ImgUrl = entity.ImgUrl,
            Name = entity.Name,
            Surname = entity.Surname,
            BirthDate = entity.BirthDate,
            CanBeMaster = entity.CanBeMaster,
            Email = entity.Email,
            Level = entity.Level
        };
        #endregion

        #region Detailed model
        public EventDetailsDTO ToDetailedModel(Event entity) => new EventDetailsDTO
        {
            EventId = entity.EventId,
            EventDate = entity.EventDate,
            EventDescription = entity.Description,
            IsPublic = entity.IsPublic,
            EventName = entity.Name,
            GameSessionsDetails = entity.GameSessions?.ConvertAll(ToDetailedModel),
        };
        public GameSessionDetailsDTO ToDetailedModel(GameSession entity) => new GameSessionDetailsDTO
        {
            GameSessionId = entity.GameSessionId,
            EventId = entity.EventId,
            MasterId = entity.MasterId,
            TableId = entity.TableId,
            IsDelete = entity.IsDeleted,
            GameSessionDate = entity.GameSessionDate,
            GameSessionEndTime = entity.GameSessionEndTime,
            Table = entity.Table != null ? ToModel(entity.Table) : null,
            TotalSeats = entity.Table != null ? entity.Table.Seat : 0,
            MasterName = entity.Master != null ? entity.Master.Name : null,
            QueueLength = entity.Reservations != null ? entity.Reservations.Count(r=>r.IsQueued) : 0,
            IsReservable = entity.Reservations != null ? entity.Reservations.Count() < entity.Table?.Seat : true,
            AvailableSeats = entity.Reservations != null && entity.Table != null 
            ? entity.Table.Seat - entity.Reservations.Count() 
            : entity.Table != null ? entity.Table.Seat : 0,
        };
        #endregion
    }
}
