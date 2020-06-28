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

namespace Application.Directors.Queries
{
    public class GetDirectorByIdQuery : IRequest<DirectorDto>
    {
        public Guid DirectorId { get; set; }
    }

    public class GetDirectorByIdQueryHandler : IRequestHandler<GetDirectorByIdQuery, DirectorDto>
    {
        private readonly IMovieDatabaseRepository _movieDatabaseRepository;
        private readonly IMapper _mapper;

        public GetDirectorByIdQueryHandler(IMovieDatabaseRepository movieDatabaseRepository, IMapper mapper)
        {
            _movieDatabaseRepository = movieDatabaseRepository;
            _mapper = mapper;
        }
        public async Task<DirectorDto> Handle(GetDirectorByIdQuery request, CancellationToken cancellationToken)
        {
            var directorFromRepo = await _movieDatabaseRepository.GetDirector(request.DirectorId);

            return directorFromRepo == null ? null : _mapper.Map<DirectorDto>(directorFromRepo);
        }
    }
}
