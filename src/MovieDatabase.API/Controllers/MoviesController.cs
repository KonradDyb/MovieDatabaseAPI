using Application.Directors;
using Application.Movies;
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

        [HttpGet("{movieId}", Name = "GetMovieForDirector")]
        public ActionResult<MovieDto> GetMovieForDirector(Guid directorId, Guid movieId)
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

        [HttpPost]
        public ActionResult<DirectorDto> CreateMovieForDirector(
            Guid directorId, MovieForCreationDto movie)
        {
            if (!_movieDatabaseRepository.DirectorExists(directorId))
            {
                return NotFound();
            }

            var movieEntity = _mapper.Map<Movie>(movie);
            _movieDatabaseRepository.AddMovie(directorId, movieEntity);
            _movieDatabaseRepository.Save();

            var movieToReturn = _mapper.Map<MovieDto>(movieEntity);

            return CreatedAtRoute("GetMovieForDirector",
                new { directorId = movieToReturn.DirectorId, movieId = movieToReturn.Id }, movieToReturn);


        }
    }
}
