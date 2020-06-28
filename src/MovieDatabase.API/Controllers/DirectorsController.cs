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
using Domain.Interfaces;
using Application.Directors.Queries;
using MediatR;
using Application.Directors.Commands;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("api/directors")]
    public class DirectorsController : ApiController
    {
        [HttpGet()]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<DirectorDto>>> GetDirectors(
            [FromQuery]DirectorsResourceParameters directorsResourceParameters)
        {
            var result = await Mediator.Send(new GetDirectorsQuery { DirectorsResourceParameters = directorsResourceParameters });

            return result != null ? (ActionResult)Ok(result) : NotFound();

        }


        [HttpGet("{directorId}", Name = "GetDirector")]
        public async Task<IActionResult> GetDirector(Guid directorId)
        {
            var result = await Mediator.Send(new GetDirectorByIdQuery{ DirectorId = directorId });

            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<DirectorDto>> CreateDirector(CreateDirectorCommand command)
        {
            var result = await Mediator.Send(command);

            return CreatedAtRoute("GetDirector", new { directorId = result.Id }, result);
        }

        [HttpOptions]
        public IActionResult GetDirectorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }
    }
}
