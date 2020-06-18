using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Movie
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        public DateTimeOffset ReleaseDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Genres { get; set; }

        [ForeignKey("DirectorId")]
        public Director Director { get; set; }

        public Guid DirectorId { get; set; }
    }
}
