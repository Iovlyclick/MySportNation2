using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNation2.Data.Models
{
    public class Participation: Entity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public CompetitionEvent CompetitionEvent { get; set; }
        public int CompetitionEventId { get; set; }

    }
}
