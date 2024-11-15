﻿using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.InputDTO
{
    public class EventInputDTO
    {
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ'\- ]{1,50}$", ErrorMessage = "Name format not valid")]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Duration { get; set; }
        public required IFormFile ImgUrl { get; set; }
        public required string AdminId { get; set; }
        public required bool IsPublic { get; set; }
    }
}