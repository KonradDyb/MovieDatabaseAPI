using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Movies.Commands.UpdateMovieForDirector
{
    public class UpdateMovieForDirectorCommand : IRequest<bool>
    {
        public Guid DirectorId { get; set; }
        public Guid MovieId { get; set; }
        public MovieForUpdateDto Movie { get; set; }
    }

    public class UpdateMovieForDirectorCommandHandler : IRequestHandler<UpdateMovieForDirectorCommand, bool>
    {
        private readonly IMovieDatabaseRepository _movieDatabaseRepository;
        private readonly IMapper _mapper;

        public UpdateMovieForDirectorCommandHandler(IMovieDatabaseRepository movieDatabaseRepository, IMapper mapper)
        {
            _movieDatabaseRepository = movieDatabaseRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateMovieForDirectorCommand request, CancellationToken cancellationToken)
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

            _mapper.Map(request.Movie, movieForDirectorFromRepo);
            _movieDatabaseRepository.UpdateMovie(movieForDirectorFromRepo);
            _movieDatabaseRepository.Save();

            return true;
        }
    }
}
