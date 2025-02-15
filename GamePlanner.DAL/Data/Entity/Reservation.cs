﻿using GamePlanner.DAL.Data.Auth;

namespace GamePlanner.DAL.Data.Entity
{
    public class Reservation
    {
        public required int ReservationId { get; set; }
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public DateTime TokenCreateDate { get; set; } = DateTime.Now;
        public required bool IsConfirmed { get; set; }
        public required bool IsNotified { get; set; } = false;
        public required bool IsDeleted { get; set; } = false;
        public required int SessionId { get; set; }
        public required string UserId { get; set; }
        public Session? Session { get; set; }
        public ApplicationUser? User { get; set; }
    }
}