using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workspace.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Inicial_Workspace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "workspace");

            migrationBuilder.CreateTable(
                name: "Documentos",
                schema: "workspace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AutorPrincipalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Resumen = table.Column<string>(type: "text", nullable: false),
                    ArchivoUrl = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coautores",
                schema: "workspace",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coautores", x => new { x.DocumentId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_Coautores_Documentos_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "workspace",
                        principalTable: "Documentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coautores",
                schema: "workspace");

            migrationBuilder.DropTable(
                name: "Documentos",
                schema: "workspace");
        }
    }
}
