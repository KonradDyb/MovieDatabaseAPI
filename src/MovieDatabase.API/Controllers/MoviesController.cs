using Application.Movies;
using AutoMapper;
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
    [Route("api/directors/{directorId}/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieDatabaseRepository _movieDatabaseRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieDatabaseRepository movieDatabaseRepository,
            IMapper mapper)
        {
            _movieDatabaseRepository = movieDatabaseRepository ?? throw new ArgumentNullException(nameof(movieDatabaseRepository));
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieDto>> GetMoviesForDirector(Guid directorId)
        {
            if (!_movieDatabaseRepository.DirectorExists(directorId))
            {
                return NotFound();
            }

            var moviesFromRepo = _movieDatabaseRepository.GetMovies(directorId);
            return Ok(_mapper.Map<IEnumerable<MovieDto>>(moviesFromRepo));
        }

        [HttpGet("{movieId}")]
        public ActionResult<MovieDto> GetMovieForAuthor(Guid directorId, Guid movieId)
        {
            if (!_movieDatabaseRepository.DirectorExists(directorId))
            {
                return NotFound();
            }

            var movieForDirectorFromRepo = _movieDatabaseRepository.GetMovie(directorId, movieId);

            if (movieForDirectorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieDto>(movieForDirectorFromRepo));
        }

        
    }
}
