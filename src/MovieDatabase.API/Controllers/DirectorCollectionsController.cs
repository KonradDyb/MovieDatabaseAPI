using Application.Common.Helpers;
using Application.DirectorCollections.Commands;
using Application.DirectorCollections.Queries;
using Application.Directors;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.API.Controllers
{
    [Route("api/directorcollections")]
    public class DirectorCollectionsController : ApiController
    {
        // parameters ids is an array of GUIDs
        // there's no implicit binding to such an array that's part of the route
        [HttpGet("({ids})", Name = "GetDirectorCollection")]
        public async Task<IActionResult> GetDirectorCollection(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var result = await Mediator.Send(new GetDirectorCollectionQuery { Ids = ids});

            return result != null ? (IActionResult)Ok(result) : NotFound();

        }

        // array key: 1,2,3
        // composite key: key1=value1, key2=value2

        [HttpPost]
        public async Task<ActionResult<IEnumerable<DirectorDto>>> CreateDirectorCollection(
           IEnumerable<DirectorForCreationDto> directors)
        {
            var result = await Mediator.Send(new CreateDirectorCollectionCommand { Directors = directors});
           
            return CreatedAtRoute("GetDirectorCollection",
                new { ids = string.Join(",", result.Select(x => x.Id))},
                result);
        }
    }
}

