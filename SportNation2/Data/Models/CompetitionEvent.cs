using static SportNation2.Infrastructure.Enumerations;

namespace SportNation2.Data.Models
{
    public class CompetitionEvent: Entity
    {
        public string Name { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }

        public CompetitionGenre Genre { get; set; }

        public int MaxParticipants { get; set; }


        public Competition Competition { get; set; }
        public int CompetitionId { get; set; }

        public List<Participation> Participations { get; set; }
        = new List<Participation>();
    }
}