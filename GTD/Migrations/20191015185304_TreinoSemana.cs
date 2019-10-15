using Microsoft.EntityFrameworkCore.Migrations;

namespace GTD.Migrations
{
    public partial class TreinoSemana : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treino_Semana_SemanaID",
                table: "Treino");

            migrationBuilder.DropIndex(
                name: "IX_Treino_SemanaID",
                table: "Treino");

            migrationBuilder.DropColumn(
                name: "SemanaID",
                table: "Treino");

            migrationBuilder.DropColumn(
                name: "DescDieta",
                table: "Dieta");

            migrationBuilder.AddColumn<string>(
                name: "DescDieta",
                table: "DietaSemana",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TreinoSemana",
                columns: table => new
                {
                    TreinoID = table.Column<int>(nullable: false),
                    SemanaID = table.Column<int>(nullable: false),
                    DescTreino = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreinoSemana", x => new { x.TreinoID, x.SemanaID });
                    table.ForeignKey(
                        name: "FK_TreinoSemana_Semana_SemanaID",
                        column: x => x.SemanaID,
                        principalTable: "Semana",
                        principalColumn: "SemanaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TreinoSemana_Treino_TreinoID",
                        column: x => x.TreinoID,
                        principalTable: "Treino",
                        principalColumn: "TreinoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreinoSemana_SemanaID",
                table: "TreinoSemana",
                column: "SemanaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreinoSemana");

            migrationBuilder.DropColumn(
                name: "DescDieta",
                table: "DietaSemana");

            migrationBuilder.AddColumn<int>(
                name: "SemanaID",
                table: "Treino",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescDieta",
                table: "Dieta",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Treino_SemanaID",
                table: "Treino",
                column: "SemanaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Treino_Semana_SemanaID",
                table: "Treino",
                column: "SemanaID",
                principalTable: "Semana",
                principalColumn: "SemanaID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
