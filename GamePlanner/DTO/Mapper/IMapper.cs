﻿using GamePlanner.DAL.Data.Entity;
using GamePlanner.DTO.InputDTO;

namespace GamePlanner.DTO.Mapper
{
    public interface IMapper
    {
        public Event ToEntity(EventInputDTO model);
        public Game ToEntity(GameInputDTO model);
        public Session ToEntity(SessionInputDTO model);
        public Reservation ToEntity(ReservationInputDTO model);
        public Knowledge ToEntity(KnowledgeInputDTO model);
        public Preference ToEntity(PreferenceInputDTO model);
    }
}
