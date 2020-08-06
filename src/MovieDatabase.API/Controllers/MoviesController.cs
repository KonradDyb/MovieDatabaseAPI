using Application.Directors;
using Application.Movies;
using Application.Movies.Commands;
using Application.Movies.Commands.CreateMovieForDirector;
using Application.Movies.Commands.PartiallyUpdateMovieForDirector;
using Application.Movies.Commands.UpdateMovieForDirector;
using Application.Movies.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieDatabase.API.Controllers
{
    [Route("api/directors/{directorId}/movies")]
    public class MoviesController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMoviesForDirector(
            Guid directorId)
        {
            var result = await Mediator.Send(new GetMoviesForDirectorQuery 
            { DirectorId = directorId});

            return result != null ? (ActionResult)Ok(result) : NotFound();
        }

        [HttpGet("{movieId}", Name = "GetMovieForDirector")]
        public async Task<ActionResult<MovieDto>> GetMovieForDirector(
            Guid directorId, Guid movieId)
        {
            var result = await Mediator.Send(new GetMovieForDirectorQuery
            { DirectorId = directorId, MovieId = movieId });

            return result != null ? (ActionResult)Ok(result): NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<DirectorDto>> CreateMovieForDirector(
            Guid directorId, MovieForCreationDto movie)
        {
            var result = await Mediator.Send(new CreateMovieForDirectorCommand 
            { Movie = movie, DirectorId = directorId });

            return CreatedAtRoute("GetMovieForDirector",
                new { directorId = result.DirectorId, movieId = result.Id }, result);
        }

        [HttpPut("{movieId}")]
        public async Task<ActionResult> UpdateMovieForDirector(Guid directorId, 
            Guid movieId,
            MovieForUpdateDto movie)
        {
            var result = await Mediator.Send(new UpdateMovieForDirectorCommand
            { DirectorId = directorId, MovieId = movieId, Movie = movie });

            return result == true ? (ActionResult)NoContent() : NotFound();
        }

        [HttpPatch("{movieId}")]
        public async Task<ActionResult> PartiallyUpdateMovieForDirector(Guid directorid,
            Guid movieId,
            JsonPatchDocument<MovieForUpdateDto> patchDocument)
        {
            var result = await Mediator.Send(new PartiallyUpdateMovieForDirectorCommand
            { DirectorId = directorid, MovieId = movieId, PatchDocument = patchDocument });

            return result == true ? (ActionResult)NoContent() : NotFound();
        }
    }
}
