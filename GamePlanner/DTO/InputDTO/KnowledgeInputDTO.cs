using GamePlanner.DAL.Data.Auth;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.InputDTO
{
    public class KnowledgeInputDTO
    {
        [MaxLength(50)]
        public required string Name { get; set; }
    }
}
