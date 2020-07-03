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
    public class MovieForCreationDto /*: IValidatableObject*/
    {
        // If property validation fails, class level validation will not occur even if it is custom attribute.

        // Custom attributes are executed  before Validate method gets called.

        [Required(ErrorMessage = "You should fill out a title.")]
        [MaxLength(100, ErrorMessage = "The title shouldn't have more than 100 characters.")]
        public string Title { get; set; }
        [MaxLength(1500, ErrorMessage = "The description shouldn't have more than 1500 characters.")]
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public string Genres { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description)
        //    {
        //        yield return new ValidationResult(
        //            "The provided description should be different from the title.",
        //            new[] { "MovieForCreationDto" });
        //    }
        //}
    }
}
