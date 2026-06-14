using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IAM.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "iam",
                table: "Roles",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Estudiante" },
                    { 2, "Asesor" },
                    { 3, "Administrador" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "iam",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "iam",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "iam",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
