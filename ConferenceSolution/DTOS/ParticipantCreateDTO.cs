﻿using System.ComponentModel.DataAnnotations;

namespace ConferenceSolution.DTOS
{
    public class ParticipantCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
