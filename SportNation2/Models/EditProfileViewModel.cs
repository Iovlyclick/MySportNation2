using SportNation2.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static SportNation2.Infrastructure.Enumerations;

namespace SportNation2.Models
{
    public class EditProfileViewModel
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date de naissance")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public UserGenre Genre { get; set; }

    }
}
