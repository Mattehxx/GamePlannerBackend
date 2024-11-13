﻿using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Db
{
    public class Game
    {
        public required int GameId { get; set; }
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ'\- ]{1,50}$", ErrorMessage = "Name format not valid")]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ImgUrl { get; set; }
        public required bool IsDisabled { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public List<Event>? Events { get; set; }
    }
}