using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportNation2.Infrastructure.Enumerations;

namespace SportNation2.Data.Models
{
    public class User : Entity
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        public string HashedPassword { get; set; }

        public DateTime BirthDate { get; set; }
        public UserGenre Genre { get; set; }


        public List<Role> Roles { get; set; }
            = new List<Role>();
        public List<Participation> Participations { get; set; }
            = new List<Participation>();
    }
}
