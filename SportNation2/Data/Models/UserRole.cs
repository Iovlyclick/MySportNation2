using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNation2.Data.Models
{
    //table de correspondance (N to N)
    public class UserRole: Entity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
