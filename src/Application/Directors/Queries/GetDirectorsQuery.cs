using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Directors.Queries
{
    public class GetDirectorsQuery : IRequest<IEnumerable<DirectorDto>>
    {
        public IDirectorsResourceParameters DirectorsResourceParameters { get; set; }
    }

    public class GetDirectorsQueryHandler : IRequestHandler
        <GetDirectorsQuery, IEnumerable<DirectorDto>>
    {
        private readonly IMovieDatabaseRepository _movieDatabaseRepository;
        private readonly IMapper _mapper;

        public GetDirectorsQueryHandler(IMovieDatabaseRepository movieDatabaseRepository, IMapper mapper)
        {
            _movieDatabaseRepository = movieDatabaseRepository;
            _mapper = mapper;
        }

        public IDirectorsResourceParameters DirectorsResourceParameters { get; set; }

        public async Task<IEnumerable<DirectorDto>> Handle(GetDirectorsQuery request, CancellationToken cancellationToken)
        {
            var directorsFromRepo = await _movieDatabaseRepository.GetDirectors(request.DirectorsResourceParameters);

            return directorsFromRepo == null ? null : _mapper.Map<IEnumerable<DirectorDto>>(directorsFromRepo);   
        }
    }
}
