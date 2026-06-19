using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workflow.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Workflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "workflow");

            migrationBuilder.CreateTable(
                name: "Revisiones",
                schema: "workflow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    AsesorId = table.Column<Guid>(type: "uuid", nullable: true),
                    Estado = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaResolucion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revisiones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComentariosRevision",
                schema: "workflow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RevisionId = table.Column<Guid>(type: "uuid", nullable: false),
                    AutorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Contenido = table.Column<string>(type: "text", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentariosRevision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComentariosRevision_Revisiones_RevisionId",
                        column: x => x.RevisionId,
                        principalSchema: "workflow",
                        principalTable: "Revisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComentariosRevision_RevisionId",
                schema: "workflow",
                table: "ComentariosRevision",
                column: "RevisionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComentariosRevision",
                schema: "workflow");

            migrationBuilder.DropTable(
                name: "Revisiones",
                schema: "workflow");
        }
    }
}
