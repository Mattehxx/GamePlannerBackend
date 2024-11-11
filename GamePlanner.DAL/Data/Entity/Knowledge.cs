using GamePlanner.DAL.Data.Auth;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Db
{
    public class Knowledge
    {
        public required int KnowledgeId { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        public List<ApplicationUser>? Users { get; set; }
    }
}