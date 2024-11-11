using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlanner.DAL.Data.Db
{
    public class Recurrence
    {
        public required int RecurrenceId { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        public List<Event>? Events { get; set; }
    }
}