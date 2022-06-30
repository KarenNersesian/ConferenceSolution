using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ConferenceSolution.Models
{
    public class Participant : IModel
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public DateTime CreatedDate { get; private set; }
        public int Age => DateTime.Now.Year - BirthDate.Year;

        public Participant()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
        }

    }
}
