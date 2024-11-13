using System.ComponentModel.DataAnnotations;

namespace GamePlanner.DAL.Data.Db
{
    public class Recurrence
    {
        public required int RecurrenceId { get; set; }
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ'\- ]{1,50}$", ErrorMessage = "Name format not valid")]
        public required string Name { get; set; }
        public required int Day { get; set; }
        public List<Event>? Events { get; set; }
    }
}