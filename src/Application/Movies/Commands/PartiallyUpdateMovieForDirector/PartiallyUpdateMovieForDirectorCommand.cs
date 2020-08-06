using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Movies.Commands.PartiallyUpdateMovieForDirector
{

    public class PartiallyUpdateMovieForDirectorCommand : IRequest<bool>
    {
        public Guid DirectorId { get; set; }
        public Guid MovieId { get; set; }
        public JsonPatchDocument<MovieForUpdateDto> PatchDocument { get; set; }
    }

    public class UpdateMovieForDirectorCommandHandler : IRequestHandler<PartiallyUpdateMovieForDirectorCommand, bool>
    {
        private readonly IMovieDatabaseRepository _movieDatabaseRepository;
        private readonly IMapper _mapper;

        public UpdateMovieForDirectorCommandHandler(IMovieDatabaseRepository movieDatabaseRepository, IMapper mapper)
        {
            _movieDatabaseRepository = movieDatabaseRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(PartiallyUpdateMovieForDirectorCommand request, CancellationToken cancellationToken)
        {

            if (!_movieDatabaseRepository.DirectorExists(request.DirectorId))
            {
                return false;
            }

            var movieForDirectorFromRepo = await _movieDatabaseRepository.GetMovie(request.DirectorId, request.MovieId);

            if (movieForDirectorFromRepo == null)
            {
                return false;
            }

            var movieToPatch = _mapper.Map<MovieForUpdateDto>(movieForDirectorFromRepo);
            // add validation
            request.PatchDocument.ApplyTo(movieToPatch);

            _mapper.Map(movieToPatch, movieForDirectorFromRepo);
            _movieDatabaseRepository.UpdateMovie(movieForDirectorFromRepo);

            _movieDatabaseRepository.Save();

            return true;
        }
    }
}
