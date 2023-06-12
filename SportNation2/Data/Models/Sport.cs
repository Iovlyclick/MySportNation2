using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNation2.Data.Models
{
    public class Sport : Entity
    {
        public Sport()
        {
            
        }
        public Sport(string sportname)
        {
            this.SportName = sportname;
        }

        public string SportName { get; set; }
    }
}
