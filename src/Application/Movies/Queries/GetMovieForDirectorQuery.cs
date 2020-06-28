using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Movies.Queries
{
    public class GetMovieForDirectorQuery : IRequest<MovieDto>
    {
        public Guid MovieId { get; set; }
        public Guid DirectorId { get; set; }
    }

    public class GetMovieForDirectorQueryHandler : IRequestHandler
        <GetMovieForDirectorQuery, MovieDto>
    {
        private readonly IMovieDatabaseRepository _movieDatabaseRepository;
        private readonly IMapper _mapper;

        public GetMovieForDirectorQueryHandler(IMovieDatabaseRepository movieDatabaseRepository, IMapper mapper)
        {
            _movieDatabaseRepository = movieDatabaseRepository;
            _mapper = mapper;
        }


        public async Task<MovieDto> Handle(GetMovieForDirectorQuery request, CancellationToken cancellationToken)
        {
            if (!_movieDatabaseRepository.DirectorExists(request.DirectorId))
            {
                return null;
            }

            var movieForDirectorFromRepo = await _movieDatabaseRepository.GetMovie(request.DirectorId, request.MovieId);

            if (movieForDirectorFromRepo == null)
            {
                return null;
            }

            return _mapper.Map<MovieDto>(movieForDirectorFromRepo);
        }
    }
}
