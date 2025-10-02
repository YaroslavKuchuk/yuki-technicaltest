using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloggingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "blog",
                table: "Authors",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("3a7d61b3-54df-44b0-9516-76282354997c"), "Michael", "Maurice" },
                    { new Guid("7b76dcd3-3e94-433e-8d55-74afeac4c33f"), "Dileep", "Sreepathi" }
                });

            migrationBuilder.InsertData(
                schema: "blog",
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Content", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("414149ff-f2ef-4979-8e33-24335dda74ce"), new Guid("3a7d61b3-54df-44b0-9516-76282354997c"), "content", "description", "How to Use the Domain Event Pattern | DDD, Clean Architecture, .NET 9" },
                    { new Guid("71d6d4ea-1b4c-47a0-9be5-6672f3dd62f0"), new Guid("7b76dcd3-3e94-433e-8d55-74afeac4c33f"), "content2", "Garbage collector", "Senior .NET Dev Interview Q/A" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "blog",
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("414149ff-f2ef-4979-8e33-24335dda74ce"));

            migrationBuilder.DeleteData(
                schema: "blog",
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71d6d4ea-1b4c-47a0-9be5-6672f3dd62f0"));

            migrationBuilder.DeleteData(
                schema: "blog",
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("3a7d61b3-54df-44b0-9516-76282354997c"));

            migrationBuilder.DeleteData(
                schema: "blog",
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("7b76dcd3-3e94-433e-8d55-74afeac4c33f"));
        }
    }
}
