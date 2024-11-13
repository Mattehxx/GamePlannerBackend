using GamePlanner.DAL.Data.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlanner.DAL.Data.Entity
{
    public class Preference
    {
        public required int PreferenceId { get; set; }
        public required bool CanBeMaster { get; set; }
        public required bool IsDeleted { get; set; }
        public required string UserId { get; set; }
        public required int KnowledgeId { get; set; }
        public required int GameId { get; set; }
        public ApplicationUser? User { get; set; }
        public Knowledge? Knowledge { get; set; }
        public Game? Game { get; set; }
    }
}
