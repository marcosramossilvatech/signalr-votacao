using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRVotacao.Migrations
{
    public partial class Ajuste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Participant1Id",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "Participant1Total",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Participant2Id",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<int>(
                name: "Participant2Total",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vote_Participant1Id",
                table: "Vote",
                column: "Participant1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_Participant2Id",
                table: "Vote",
                column: "Participant2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Participants_Participant1Id",
                table: "Vote",
                column: "Participant1Id",
                principalTable: "Participants",
                principalColumn: "ParticId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Participants_Participant2Id",
                table: "Vote",
                column: "Participant2Id",
                principalTable: "Participants",
                principalColumn: "ParticId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Participants_Participant1Id",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Participants_Participant2Id",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_Participant1Id",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_Participant2Id",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "Participant1Id",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "Participant1Total",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "Participant2Id",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "Participant2Total",
                table: "Vote");
        }
    }
}
