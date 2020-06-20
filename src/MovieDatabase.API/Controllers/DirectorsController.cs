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
            return new JsonResult(directorsFromRepo);
        }


        [HttpGet("{directorId}")]
        public IActionResult GetDirector(Guid directorId)
        {
            var directorFromRepo = _movieDatabaseRepository.GetDirector(directorId);
            return new JsonResult(directorFromRepo);
        }
    }
}
