using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Web.Alunos.Migrations
{
    /// <inheritdoc />
    public partial class AddRepresentantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Representantes",
                columns: table => new
                {
                    RepresentanteId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomeRepresentante = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CPF = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Representantes", x => x.RepresentanteId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Representantes_CPF",
                table: "Representantes",
                column: "CPF",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Representantes");
        }
    }
}
