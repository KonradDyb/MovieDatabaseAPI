using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Movies.Queries
{
    public class GetMoviesForDirectorQuery : IRequest<IEnumerable<MovieDto>>
    {
        public Guid DirectorId { get; set; }
    }

    public class GetMoviesForDirectorQueryHandler : IRequestHandler
        <GetMoviesForDirectorQuery, IEnumerable<MovieDto>>
    {
        private readonly IMovieDatabaseRepository _movieDatabaseRepository;
        private readonly IMapper _mapper;

        public GetMoviesForDirectorQueryHandler(IMovieDatabaseRepository movieDatabaseRepository, IMapper mapper)
        {
            _movieDatabaseRepository = movieDatabaseRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<MovieDto>> Handle(GetMoviesForDirectorQuery request, CancellationToken cancellationToken)
        {
            if (!_movieDatabaseRepository.DirectorExists(request.DirectorId))
            {
                return null;
            }

            var moviesFromRepo = await _movieDatabaseRepository.GetMovies(request.DirectorId);
            return _mapper.Map<IEnumerable<MovieDto>>(moviesFromRepo);
        }
    }
}
