using Application.Directors;
using Application.Common;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("api/directors")]
    public class DirectorsController : ControllerBase
    {
        private readonly IMovieDatabaseRepository _movieDatabaseRepository;
        private readonly IMapper _mapper;

        public DirectorsController(IMovieDatabaseRepository movieDatabaseRepository,
            IMapper mapper)
        {
            _movieDatabaseRepository = movieDatabaseRepository ?? throw new ArgumentNullException(nameof(movieDatabaseRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<DirectorDto>> GetDirectors()
        {
            var directorsFromRepo = _movieDatabaseRepository.GetDirectors();
            return Ok(_mapper.Map<IEnumerable<DirectorDto>>(directorsFromRepo));
        }


        [HttpGet("{directorId}")]
        public IActionResult GetDirector(Guid directorId)
        {
            var directorFromRepo = _movieDatabaseRepository.GetDirector(directorId);

            if(directorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DirectorDto>(directorFromRepo));
        }
    }
}
