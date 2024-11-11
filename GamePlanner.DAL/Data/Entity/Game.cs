﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlanner.DAL.Data.Db
{
    public class Game
    {
        public required int GameId { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ImgUrl { get; set; }
        public required bool IsDisabled { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public List<Event>? Events { get; set; }
    }
}