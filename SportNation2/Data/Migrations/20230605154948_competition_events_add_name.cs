using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportNation2.Data.Migrations
{
    /// <inheritdoc />
    public partial class competition_events_add_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Sport_SportId",
                table: "Competitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sport",
                table: "Sport");

            migrationBuilder.RenameTable(
                name: "Sport",
                newName: "Sports");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Competitions",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CompetitionEvents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sports",
                table: "Sports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Sports_SportId",
                table: "Competitions",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Sports_SportId",
                table: "Competitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sports",
                table: "Sports");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CompetitionEvents");

            migrationBuilder.RenameTable(
                name: "Sports",
                newName: "Sport");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sport",
                table: "Sport",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Sport_SportId",
                table: "Competitions",
                column: "SportId",
                principalTable: "Sport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
