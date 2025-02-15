﻿using GamePlanner.DAL.Data.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlanner.DAL.Data.Entity
{
    public class Event
    {
        public int EventId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool IsPublic { get; set; }
        public required string ImgUrl { get; set; }
        public required bool IsDeleted { get; set; } = false;
        [ForeignKey("AdminUser")]
        public required string AdminId { get; set; }
        public ApplicationUser? AdminUser { get; set; }
        public List<Session>? Sessions { get; set; }
    }
}