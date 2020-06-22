using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Services
{
    public interface IMovieDatabaseRepository
    {    
        IEnumerable<Movie> GetMovies(Guid directorId);
        Movie GetMovie(Guid directorId, Guid movieId);
        void AddMovie(Guid directorId, Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(Movie movie);
        IEnumerable<Director> GetDirectors();
        Director GetDirector(Guid directorId);
        IEnumerable<Director> GetDirectors(IEnumerable<Guid> directorIds);
        IEnumerable<Director> GetDirectors(int yearOfBirth);
        void AddDirector(Director director);
        void DeleteDirector(Director director);
        void UpdateDirector(Director director);
        bool DirectorExists(Guid directorId);
        bool Save();
    }
}
