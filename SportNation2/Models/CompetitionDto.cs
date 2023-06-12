using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNation2.Models
{
    public class CompetitionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Events { get; set; } = new List<string>();

    }
}
