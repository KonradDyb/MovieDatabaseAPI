using Application.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movies
{
 
    public class MovieForCreationDto : MovieForManipulationDto
        /*: IValidatableObject*/
    {
        // If property validation fails, class level validation will not occur even if it is custom attribute.

        // Custom attributes are executed  before Validate method gets called.

        
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
