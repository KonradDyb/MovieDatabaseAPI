using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Persistence.DbContexts
{
    public class MovieDatabaseContext : DbContext
    {
        public MovieDatabaseContext(DbContextOptions<MovieDatabaseContext> options)
           : base(options)
        {
        }

        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Director>().HasData(
                new Director()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    FirstName = "Todd",
                    LastName = "Phillips",
                    DateOfBirth = new DateTime(1970, 12, 20),
                    PlaceOfBirth = "Brooklyn, New York City, New York, USA"
                },
                new Director()
                {
                    Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    FirstName = "Bong",
                    LastName = "Joon Ho",
                    DateOfBirth = new DateTime(1969, 9, 14),
                    PlaceOfBirth = "Daegu, South Korea"
                },
                new Director()
                {
                    Id = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                    FirstName = "Mel",
                    LastName = "Gibson",
                    DateOfBirth = new DateTime(1956, 1, 3),
                    PlaceOfBirth = "Peekskill, New York, USA"
                },
                new Director()
                {
                    Id = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"),
                    FirstName = "Wojciech",
                    LastName = "Smarzowski",
                    DateOfBirth = new DateTime(1963, 1, 18),
                    PlaceOfBirth = "Korczyna, Podkarpackie, Poland"
                },
                new Director()
                {
                    Id = Guid.Parse("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"),
                    FirstName = "Neill",
                    LastName = "Blomkamp",
                    DateOfBirth = new DateTime(1979, 9, 17),
                    PlaceOfBirth = "Johannesburg, South Africa"
                },
                new Director()
                {
                    Id = Guid.Parse("2aadd2df-7caf-45ab-9355-7f6332985a87"),
                    FirstName = "Ridley",
                    LastName = "Scott",
                    DateOfBirth = new DateTime(1937, 10, 30),
                    PlaceOfBirth = "South Shields, County Durham, England, UK"
                },
                new Director()
                {
                    Id = Guid.Parse("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"),
                    FirstName = "Quentin",
                    LastName = "Tarantino",
                    DateOfBirth = new DateTime(1963, 3, 27),
                    PlaceOfBirth = "Knoxville, Tennessee, USA"
                }
                );

            modelBuilder.Entity<Movie>().HasData(
               new Movie
               {
                   Id = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
                   DirectorId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                   Title = "Joker",
                   Description = "In Gotham City, mentally troubled comedian Arthur Fleck is disregarded and mistreated by society. He then embarks on a downward spiral of revolution and bloody crime. This path brings him face-to-face with his alter-ego: the Joker..",
                  ReleaseDate = new DateTime(2019, 10, 4),
                  Genres = "Crime, Drama, Thriller"
               },
               new Movie
               {
                   Id = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
                   DirectorId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                   Title = "The Hangover",
                   Description = "Three buddies wake up from a bachelor party in Las Vegas, with no memory of the previous night and the bachelor missing. They make their way around the city in order to find their friend before his wedding.",
                   ReleaseDate = new DateTime(2009, 6, 12),
                   Genres = "Comedy"

               },
               new Movie
               {
                   Id = Guid.Parse("d173e20d-159e-4127-9ce9-b0ac2564ad97"),
                   DirectorId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                   Title = "Parasite",
                   Description = "Greed and class discrimination threaten the newly formed symbiotic relationship between the wealthy Park family and the destitute Kim clan.",
                   ReleaseDate = new DateTime(2020, 2, 7),
                   Genres = "Comedy, Drama, Thriller"
               },
               new Movie
               {
                   Id = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869a"),
                   DirectorId = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                   Title = "Hacksaw Ridge",
                   Description = "World War II American Army Medic Desmond T. Doss, who served during the Battle of Okinawa, refuses to kill people, and becomes the first man in American history to receive the Medal of Honor without firing a shot.",
                   ReleaseDate = new DateTime(2017, 1, 26),
                   Genres = "Biography, Drama, History"
               }
               );

            base.OnModelCreating(modelBuilder);
        }
    }
}
