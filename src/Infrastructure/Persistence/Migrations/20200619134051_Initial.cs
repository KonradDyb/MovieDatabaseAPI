using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(nullable: false),
                    PlaceOfBirth = table.Column<string>(nullable: false),
                    DateOfDeath = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1500, nullable: false),
                    ReleaseDate = table.Column<DateTimeOffset>(nullable: false),
                    Genres = table.Column<string>(maxLength: 100, nullable: false),
                    DirectorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "DateOfBirth", "DateOfDeath", "FirstName", "LastName", "PlaceOfBirth" },
                values: new object[,]
                {
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new DateTimeOffset(new DateTime(1970, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), null, "Todd", "Phillips", "Brooklyn, New York City, New York, USA" },
                    { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), new DateTimeOffset(new DateTime(1969, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, "Bong", "Joon Ho", "Daegu, South Korea" },
                    { new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), new DateTimeOffset(new DateTime(1956, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), null, "Mel", "Gibson", "Peekskill, New York, USA" },
                    { new Guid("102b566b-ba1f-404c-b2df-e2cde39ade09"), new DateTimeOffset(new DateTime(1963, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), null, "Wojciech", "Smarzowski", "Korczyna, Podkarpackie, Poland" },
                    { new Guid("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"), new DateTimeOffset(new DateTime(1979, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, "Neill", "Blomkamp", "Johannesburg, South Africa" },
                    { new Guid("2aadd2df-7caf-45ab-9355-7f6332985a87"), new DateTimeOffset(new DateTime(1937, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, "Ridley", "Scott", "South Shields, County Durham, England, UK" },
                    { new Guid("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"), new DateTimeOffset(new DateTime(1963, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), null, "Quentin", "Tarantino", "Knoxville, Tennessee, USA" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "DirectorId", "Genres", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), "In Gotham City, mentally troubled comedian Arthur Fleck is disregarded and mistreated by society. He then embarks on a downward spiral of revolution and bloody crime. This path brings him face-to-face with his alter-ego: the Joker..", new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "Crime, Drama, Thriller", new DateTimeOffset(new DateTime(2019, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Joker" },
                    { new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), "Three buddies wake up from a bachelor party in Las Vegas, with no memory of the previous night and the bachelor missing. They make their way around the city in order to find their friend before his wedding.", new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "Comedy", new DateTimeOffset(new DateTime(2009, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "The Hangover" },
                    { new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad97"), "Greed and class discrimination threaten the newly formed symbiotic relationship between the wealthy Park family and the destitute Kim clan.", new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "Comedy, Drama, Thriller", new DateTimeOffset(new DateTime(2020, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Parasite" },
                    { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"), "World War II American Army Medic Desmond T. Doss, who served during the Battle of Okinawa, refuses to kill people, and becomes the first man in American history to receive the Medal of Honor without firing a shot.", new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), "Biography, Drama, History", new DateTimeOffset(new DateTime(2017, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Hacksaw Ridge" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies",
                column: "DirectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Directors");
        }
    }
}
