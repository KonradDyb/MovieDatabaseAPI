using Application.Directors;
using Application.Common;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("api/directors")]
    public class DirectorsController : ControllerBase
    {
        private readonly IMovieDatabaseRepository _movieDatabaseRepository;

        public DirectorsController(IMovieDatabaseRepository movieDatabaseRepository)
        {
            _movieDatabaseRepository = movieDatabaseRepository ?? throw new ArgumentNullException(nameof(movieDatabaseRepository));

        }

        [HttpGet()]
        public IActionResult GetDirectors()
        {
            var directorsFromRepo = _movieDatabaseRepository.GetDirectors();
            var directors = new List<DirectorDto>();

            foreach (var director in directorsFromRepo)
            {
                directors.Add(new DirectorDto()
                {
                    Id = director.Id,
                    Name = $"{director.FirstName} {director.LastName}",
                    PlaceOfBirth = director.PlaceOfBirth,
                    Age = director.DateOfBirth.GetCurrentAge()
                }); 

            }
            return Ok(directors);
        }


        [HttpGet("{directorId}")]
        public IActionResult GetDirector(Guid directorId)
        {
            var directorFromRepo = _movieDatabaseRepository.GetDirector(directorId);

            if(directorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(directorFromRepo);
        }
    }
}
