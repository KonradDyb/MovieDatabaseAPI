using Application.Directors;
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

namespace Application.DirectorCollections.Commands
{
    public class CreateDirectorCollectionCommand : IRequest<IEnumerable<DirectorDto>>
    {
        public IEnumerable<DirectorForCreationDto> Directors { get; set; }
    }

    public class CreateDirectorCollectionCommandHandler : IRequestHandler<CreateDirectorCollectionCommand, IEnumerable<DirectorDto>>
    {
        private readonly IMovieDatabaseRepository _movieDatabaseRepository;
        private readonly IMapper _mapper;

        public CreateDirectorCollectionCommandHandler(IMovieDatabaseRepository movieDatabaseRepository, IMapper mapper)
        {
            _movieDatabaseRepository = movieDatabaseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DirectorDto>> Handle(CreateDirectorCollectionCommand request, CancellationToken cancellationToken)
        {
            var directorEntities = _mapper.Map<IEnumerable<Director>>(request.Directors);
            foreach (var director in directorEntities)
            {
                await _movieDatabaseRepository.AddDirector(director);
            }

            _movieDatabaseRepository.Save();
            return _mapper.Map<IEnumerable<DirectorDto>>(directorEntities);
        }
    }
}
