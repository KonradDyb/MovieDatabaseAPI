using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Movies.Commands.CreateMovieForDirector
{
    public class CreateMovieForDirectorCommand : IRequest<MovieDto>
    {
        public Guid DirectorId { get; set; }
        public MovieForCreationDto Movie { get; set; }
    }

    public class CreateMovieForDirectorCommandHandler : IRequestHandler
        <CreateMovieForDirectorCommand, MovieDto>
    {
        private readonly IMovieDatabaseRepository _movieDatabaseRepository;
        private readonly IMapper _mapper;

        public CreateMovieForDirectorCommandHandler(IMovieDatabaseRepository movieDatabaseRepository, IMapper mapper)
        {
            _movieDatabaseRepository = movieDatabaseRepository;
            _mapper = mapper;
        }

        public async Task<MovieDto> Handle(CreateMovieForDirectorCommand request, CancellationToken cancellationToken)
        {
            if (!_movieDatabaseRepository.DirectorExists(request.DirectorId))
            {
                return null;
            }

            var movieEntity = _mapper.Map<Movie>(request.Movie);
            await _movieDatabaseRepository.AddMovie(request.DirectorId, movieEntity);
            _movieDatabaseRepository.Save();

            var movieToReturn = _mapper.Map<MovieDto>(movieEntity);
            return movieToReturn;
        }
    }
}
