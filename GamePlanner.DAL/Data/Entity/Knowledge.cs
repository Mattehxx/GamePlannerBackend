﻿using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Entity
{
    public class Knowledge
    {
        public required int KnowledgeId { get; set; }
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ'\- ]{1,50}$", ErrorMessage = "Name format not valid")]
        public required string Name { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public List<Preference>? Preferences { get; set; }
    }
}
