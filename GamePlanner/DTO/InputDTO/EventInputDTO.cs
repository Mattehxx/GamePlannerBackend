using GamePlanner.DAL.Data.Auth;
using GamePlanner.DAL.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DTO.InputDTO
{
    public class EventInputDTO
    {
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ'\- ]{1,50}$", ErrorMessage = "Name format not valid")]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required DateTime EventStartDate { get; set; }
        public required DateTime EventEndDate { get; set; }
        public required int Duration { get; set; }
        public required bool IsPublic { get; set; }
        public required string ImgUrl { get; set; }
        public required bool IsDeleted { get; set; } = false;
        public required string AdminId { get; set; }
        public required int GameId { get; set; }
        public required int RecurrenceId { get; set; }
    }
}