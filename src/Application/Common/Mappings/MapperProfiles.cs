using Application.Directors;
using Application.Directors.Commands;
using Application.Movies;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mappings
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Director, DirectorDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge()));

            CreateMap<Movie, MovieDto>()
                .ForMember(
                dest => dest.ReleaseDate,
                opt => opt.MapFrom(src => src.ReleaseDate.ToString("d MMMM yyyy")));

            CreateMap<DirectorForCreationDto, Director>();
            CreateMap<MovieForCreationDto, Movie>();
            CreateMap<CreateDirectorCommand, Director>();
            CreateMap<MovieForUpdateDto, Movie>();
        }
    }
}
