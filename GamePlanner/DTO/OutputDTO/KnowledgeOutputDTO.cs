using GamePlanner.DAL.Data.Auth;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.OutputDTO
{
    public class KnowledgeOutputDTO
    {
        public required int KnowledgeId { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        public List<ApplicationUserOutputDTO>? Users { get; set; }
    }
}
