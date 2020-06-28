using Application.Movies;
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

namespace Application.Directors.Commands
{
    public class CreateDirectorCommand : IRequest<DirectorDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public ICollection<MovieForCreationDto> Movies { get; set; }
         = new List<MovieForCreationDto>();
    }

    public class CreateDirectorCommandHandler : IRequestHandler 
        <CreateDirectorCommand, DirectorDto>
    {
        private readonly IMovieDatabaseRepository _movieDatabaseRepository;
        private readonly IMapper _mapper;

        public CreateDirectorCommandHandler(IMovieDatabaseRepository movieDatabaseRepository, IMapper mapper)
        {
            _movieDatabaseRepository = movieDatabaseRepository;
            _mapper = mapper;
        }

        public async Task<DirectorDto> Handle(CreateDirectorCommand request, 
            CancellationToken cancellationToken)
        {
            var directorEntity = _mapper.Map<Director>(request);
            //At this moment, the entity hasnt been added to database.
            //It's been added on the DbContext, which represents a session with database.
            await _movieDatabaseRepository.AddDirector(directorEntity);
            _movieDatabaseRepository.Save();

            return _mapper.Map<DirectorDto>(directorEntity);
        }
    }
}
