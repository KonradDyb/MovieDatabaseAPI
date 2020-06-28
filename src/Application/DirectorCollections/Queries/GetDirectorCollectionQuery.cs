using Application.Directors;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DirectorCollections.Queries
{
    public class GetDirectorCollectionQuery : IRequest<IEnumerable<DirectorDto>>
    {
        public IEnumerable<Guid> Ids { get; set; }
    }

    public class GetDirectorCollectionQueryHandler : IRequestHandler
        <GetDirectorCollectionQuery, IEnumerable<DirectorDto>>
    {
        private readonly IMovieDatabaseRepository _movieDatabaseRepository;
        private readonly IMapper _mapper;

        public GetDirectorCollectionQueryHandler(IMovieDatabaseRepository movieDatabaseRepository, IMapper mapper)
        {
            _movieDatabaseRepository = movieDatabaseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DirectorDto>> Handle(GetDirectorCollectionQuery request, CancellationToken cancellationToken)
        {
            if(request.Ids == null)
            {
                return null;
            }

            var directorEntities = await _movieDatabaseRepository.GetDirectors(request.Ids);

            if (request.Ids.Count() != directorEntities.Count())
            {
                return null;
            }

            return _mapper.Map<IEnumerable<DirectorDto>>(directorEntities);
        }
    }
}
