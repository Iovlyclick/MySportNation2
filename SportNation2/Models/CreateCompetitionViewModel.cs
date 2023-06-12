using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNation2.Models
{
    public class CreateCompetitionViewModel
    {        
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public List<(int Id, string Name)> Sports { get; set; }
    }
}
