using Application.Common.Helpers;
using Application.Directors;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
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
    [Route("api/directorcollections")]
    public class DirectorCollectionsController : ControllerBase
    {
        private readonly IMovieDatabaseRepository _movieDatabaseRepository;
        private readonly IMapper _mapper;

        public DirectorCollectionsController(IMovieDatabaseRepository movieDatabaseRepository,
            IMapper mapper)
        {
            _movieDatabaseRepository = movieDatabaseRepository ?? throw new ArgumentNullException(nameof(movieDatabaseRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // parameters ids is an array of GUIDs
        // there's no implicit binding to such an array that's part of the route
        [HttpGet("({ids})", Name = "GetDirectorCollection")]
        public IActionResult GetDirectorCollection(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var directorEntities = _movieDatabaseRepository.GetDirectors(ids);

            if (ids.Count() != directorEntities.Count())
            {
                return NotFound();
            }

            var directorsToReturn = _mapper.Map<IEnumerable<DirectorDto>>(directorEntities);

            return Ok(directorsToReturn);

        }


        // array key: 1,2,3
        // composite key: key1=value1, key2=value2

        [HttpPost]
        public ActionResult<IEnumerable<DirectorDto>> CreateDirectorCollection(
            IEnumerable<DirectorForCreationDto> directorCollection)
        {
            var directorEntities = _mapper.Map<IEnumerable<Director>>(directorCollection);
            foreach (var director in directorEntities)
            {
                _movieDatabaseRepository.AddDirector(director);
            }

            _movieDatabaseRepository.Save();

            var directorCollectionToReturn = _mapper.Map<IEnumerable<DirectorDto>>(directorEntities);
            var idsAsString = string.Join(",", directorCollectionToReturn.Select(x => x.Id));

            return CreatedAtRoute("GetDirectorCollection",
                new { ids = idsAsString },
                directorCollectionToReturn);
        }
    }
}

