using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNation2.Data.Models
{
    public class Role : Entity
    {
        [Required, StringLength(20)]
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
            = new List<User>();
    }
}
