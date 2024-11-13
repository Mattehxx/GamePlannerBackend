using GamePlanner.DAL.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.OutputDTO.GeneralDTO
{
    public class KnowledgeOutputDTO
    {
        public required int KnowledgeId { get; set; }
        public required string Name { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
