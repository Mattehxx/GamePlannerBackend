﻿using GamePlanner.DAL.Data.Db;

namespace GamePlanner.DTO.OutputDTO
{
    public class ApplicationUserOutputDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string? ImgUrl { get; set; }
        public bool CanBeMaster { get; set; }
        public int KnowledgeId { get; set; }
        public int Level { get; set; }
        //public List<GameSession>? MasterGameSessions { get; set; }
        //public List<Reservation>? Reservations { get; set; }
        //public List<Event>? AdminEvents { get; set; }
    }
}