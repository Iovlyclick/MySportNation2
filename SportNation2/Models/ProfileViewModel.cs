using System;
using System.Collections.Generic;
using SportNation2.Data.Models;
using SportNation2.Infrastructure;

namespace SportNation2.Models
{
    public class ProfileViewModel
    {
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public UserGenre Genre { get; set; }
        public List<string> Roles { get; set; }
        public List<Participation> Participations { get; set; }
    }
}
