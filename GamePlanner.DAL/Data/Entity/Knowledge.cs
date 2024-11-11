using GamePlanner.DAL.Data.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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