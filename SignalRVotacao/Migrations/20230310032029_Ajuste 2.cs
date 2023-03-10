using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRVotacao.Migrations
{
    public partial class Ajuste2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Participant3Id",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddColumn<int>(
                name: "Participant3Total",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vote_Participant3Id",
                table: "Vote",
                column: "Participant3Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Participants_Participant3Id",
                table: "Vote",
                column: "Participant3Id",
                principalTable: "Participants",
                principalColumn: "ParticId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Participants_Participant3Id",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_Participant3Id",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "Participant3Id",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "Participant3Total",
                table: "Vote");
        }
    }
}
