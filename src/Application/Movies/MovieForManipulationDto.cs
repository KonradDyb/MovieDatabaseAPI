using Application.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movies
{
    [MovieTitleMustBeDifferentFromDescription
    (ErrorMessage = "Title must be different from description")]
    public abstract class MovieForManipulationDto
    {
        [Required(ErrorMessage = "You should fill out a Title.")]
        [MaxLength(100, ErrorMessage = "The title shouldn't have more than 100 characters.")]
        public string Title { get; set; }
        [MaxLength(1500, ErrorMessage = "The description shouldn't have more than 1500 characters.")]
        [Required(ErrorMessage = "You should fill out a Description.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "You should fill out a ReleaseDate.")]
        public string ReleaseDate { get; set; }
        [Required(ErrorMessage = "You should fill out a Genres.")]
        public string Genres { get; set; }
    }
}
