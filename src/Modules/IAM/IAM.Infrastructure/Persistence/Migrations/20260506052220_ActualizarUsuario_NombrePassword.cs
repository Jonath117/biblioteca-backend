using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAM.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarUsuario_NombrePassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreCompleto",
                schema: "iam",
                table: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                schema: "iam",
                table: "Usuarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                schema: "iam",
                table: "Usuarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                schema: "iam",
                table: "Usuarios",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                schema: "iam",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Nombre",
                schema: "iam",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                schema: "iam",
                table: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                schema: "iam",
                table: "Usuarios",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
