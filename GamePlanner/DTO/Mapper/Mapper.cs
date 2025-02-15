﻿using GamePlanner.DAL.Data.Entity;
using GamePlanner.DTO.InputDTO;
using GamePlanner.Services.IServices;

namespace GamePlanner.DTO.Mapper
{
    public class Mapper : IMapper
    {
        private readonly IBlobService _blobService;
        public Mapper(IBlobService blobService) => _blobService = blobService;

        #region ToEntity
        public Event ToEntity(EventInputDTO model)  =>  new Event
        {
            EventId = 0,
            AdminId = model.AdminId,
            IsDeleted = false,
            Description = model.Description,
            ImgUrl = _blobService.UploadFile(_blobService.GetBlobContainerClient("event-container"),model.Image),
            IsPublic = model.IsPublic,
            Name = model.Name,
            
        };
        public Game ToEntity(GameInputDTO model) => new Game
        {
            GameId = 0,
            IsDeleted = false,
            IsDisabled = false,
            Description = model.Description,
            ImgUrl = _blobService.UploadFile(_blobService.GetBlobContainerClient("game-container"), model.ImgUrl),
            Name = model.Name,
        };
        public Session ToEntity(SessionInputDTO model) => new Session
        {
            SessionId = 0,
            EventId = model.EventId,
            MasterId = model.MasterId,
            IsDeleted = false,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            GameId = model.GameId,
            Seats = model.Seats,
        };
        public Reservation ToEntity(ReservationInputDTO model) => new Reservation
        {
            ReservationId = 0,
            SessionId = model.SessionId,
            UserId = model.UserId,
            IsDeleted = false,
            IsConfirmed = false,
            IsNotified = false,
        };
        public Knowledge ToEntity(KnowledgeInputDTO model) => new Knowledge
        {
            KnowledgeId = 0,
            IsDeleted = false,
            Name = model.Name,
        };
        public Preference ToEntity(PreferenceInputDTO model) => new Preference
        {
            PreferenceId = 0,
            KnowledgeId = model.KnowledgeId,
            GameId = model.GameId,
            UserId = model.UserId,
            IsDeleted = false,
            CanBeMaster = model.CanBeMaster,
        };
        #endregion


        #region ToModel
        //public EventOutputDTO ToModel(Event entity) => new EventOutputDTO
        //{
        //    AdminId = entity.AdminId,
        //    EventId = 0,
        //    //GameId = entity.,
        //    IsDeleted = entity.IsDeleted,
        //    Description = entity.Description,
        //    EventEndDate = entity.Sessions != null ? entity.Sessions.Max(s => s.EndDate) : null,
        //    EventStartDate = entity.Sessions != null ? entity.Sessions.Min(s => s.StartDate) : null,
        //    ImgUrl = entity.ImgUrl,
        //    IsPublic = entity.IsPublic,
        //    Name = entity.Name,
        //};
        //public GameOutputDTO ToModel(Game entity) => new GameOutputDTO
        //{
        //    GameId = 0,
        //    IsDeleted = entity.IsDeleted,
        //    IsDisabled = entity.IsDisabled,
        //    Description = entity.Description,
        //    ImgUrl = entity.ImgUrl,
        //    Name = entity.Name,
        //};
        //public SessionOutputDTO ToModel(Session entity) => new SessionOutputDTO
        //{
        //    SessionId = entity.SessionId,
        //    Seats = entity.Seats,
        //    StartDate = entity.StartDate,
        //    EndDate = entity.EndDate,
        //    EventId = entity.EventId,
        //    GameId = entity.GameId,
        //    IsDeleted = entity.IsDeleted,
        //    MasterId = entity.MasterId,
        //};
        //public ReservationOutputDTO ToModel(Reservation entity) => new ReservationOutputDTO
        //{
        //    ReservationId = entity.ReservationId,
        //    SessionId = entity.SessionId,
        //    IsConfirmed = entity.IsConfirmed,
        //    IsDelete = entity.IsDeleted,
        //    UserId = entity.UserId,
        //    Name = entity.User is not null ? entity.User.Name : null,
        //    Surname = entity.User is not null ? entity.User.Surname : null,
        //    Email = entity.User is not null ? entity.User.Email : null,
        //};
        //public UserOutputDTO ToModel(ApplicationUser entity) => new UserOutputDTO
        //{
        //    Id = entity.Id,
        //    Name = entity.Name,
        //    Surname = entity.Surname,
        //    Email = entity.Email,
        //    ImgUrl = entity.ImgUrl,
        //    Level = entity.Level,
        //    Preferences = entity.Preferences?.ConvertAll(ToModel),
        //};
        //public PreferenceOutputDTO ToModel(Preference entity) => new PreferenceOutputDTO
        //{
        //    PreferenceId = entity.PreferenceId,
        //    GameId = entity.GameId,
        //    KnowledgeId = entity.KnowledgeId,
        //    UserId = entity.UserId,
        //    IsDeleted = entity.IsDeleted,
        //    CanBeMaster = entity.CanBeMaster,
        //    GameName = entity.Game is not null ? entity.Game.Name : null,
        //    KnowledgeName = entity.Knowledge is not null ? entity.Knowledge.Name : null,

        //};
        //public KnowledgeOutputDTO ToModel(Knowledge entity) => new KnowledgeOutputDTO
        //{
        //    KnowledgeId = entity.KnowledgeId,
        //    IsDeleted = entity.IsDeleted,
        //    Name = entity.Name,
        //};
        //#endregion

        //#region Detailed model
        //public EventDetailsDTO ToDetailedModel(Event entity)
        //{
        //    var eventDetails = ToModel(entity);
        //    return new EventDetailsDTO
        //    {
        //        AdminId = eventDetails.AdminId,
        //        EventId = eventDetails.EventId,
        //        IsDeleted = eventDetails.IsDeleted,
        //        IsPublic = eventDetails.IsPublic,
        //        Description = eventDetails.Description,
        //        ImgUrl = eventDetails.ImgUrl,
        //        Name = eventDetails.Name,
        //        EventStartDate = eventDetails.EventStartDate,
        //        EventEndDate = eventDetails.EventEndDate,
        //        SessionsDetails = entity.Sessions?.ConvertAll(ToDetailedModel)
        //    };
        //}
        //public SessionDetailsDTO ToDetailedModel(Session entity) => new SessionDetailsDTO
        //{
        //    EventId = entity.EventId,
        //    GameId = entity.GameId,
        //    MasterId = entity.MasterId,
        //    SessionId = entity.SessionId,
        //    IsDeleted = entity.IsDeleted,
        //    Seats = entity.Seats,
        //    StartDate = entity.StartDate,
        //    EndDate = entity.EndDate,
        //    Master = entity.Master is not null ? ToModel(entity.Master) : null,
        //    TotalSeats = entity.Seats,
        //    QueueLength = entity.Reservations?.Count(res => !res.IsConfirmed) ?? 0,
        //    Reservations = entity.Reservations?.ConvertAll(ToModel),
        //    AvailableSeats = entity.Seats - (entity.Reservations is not null ? entity.Reservations.Count() : 0),
        //};
        #endregion
    }
}
