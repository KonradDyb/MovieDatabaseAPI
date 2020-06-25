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
using Infrastructure.Persistence.ResourceParameters;
using Domain.Entities;

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
        public ActionResult<IEnumerable<DirectorDto>> GetDirectors(
            [FromQuery]DirectorsResourceParameters directorsResourceParameters)
        {
            var directorsFromRepo = _movieDatabaseRepository.GetDirectors(directorsResourceParameters);
            return Ok(_mapper.Map<IEnumerable<DirectorDto>>(directorsFromRepo));
        }


        [HttpGet("{directorId}", Name = "GetDirector")]
        public IActionResult GetDirector(Guid directorId)
        {
            var directorFromRepo = _movieDatabaseRepository.GetDirector(directorId);

            if(directorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DirectorDto>(directorFromRepo));
        }

        [HttpPost]
        public ActionResult<DirectorDto> CreateDirector(DirectorForCreationDto director)
        {
            var directorEntity = _mapper.Map<Director>(director);
            // At this moment, the entity hasnt been added to database.
            // It's been added on the DbContext, which represents a session with database.
             _movieDatabaseRepository.AddDirector(directorEntity);
            _movieDatabaseRepository.Save();

            var directorToReturn = _mapper.Map<DirectorDto>(directorEntity);
            return CreatedAtRoute("GetDirector", new { directorId = directorToReturn.Id },
                directorToReturn);
        }
    }
}
