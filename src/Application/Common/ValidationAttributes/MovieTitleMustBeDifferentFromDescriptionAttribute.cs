using Application.Movies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class MovieTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var movie = (MovieForManipulationDto)validationContext.ObjectInstance;

            if (movie.Title == movie.Description)
            {
                return new ValidationResult(ErrorMessage,
                    new[] { nameof(MovieForManipulationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
