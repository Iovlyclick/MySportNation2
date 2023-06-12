using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNation2.Models
{
    public class AddCompetitionEventViewModel
    {
        public string CompetitionName { get; set; }
        [DisplayName("Titre")]
        public string Name { get; set; }
        [DisplayName("Age minimum")]
        public int MinAge { get; set; }
        [DisplayName("Age maximum")]
        public int MaxAge { get; set; }
        [DisplayName("Nombre max de participants")]
        public int MaxParticipants { get; set; }
        [DisplayName("Genre")]
        public string[] Genres { get; set; }
        public int CompetitionId { get; set; }
        [DisplayName("Epreuves de la compétition")]
        public List<string> CompetitionEvents { get; set; }
            = new List<string>();

    }
}
