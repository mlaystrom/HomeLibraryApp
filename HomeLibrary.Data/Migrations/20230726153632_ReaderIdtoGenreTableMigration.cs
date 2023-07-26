using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReaderIdtoGenreTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReaderId",
                table: "Genre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_ReaderId",
                table: "Genre",
                column: "ReaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Reader_ReaderId",
                table: "Genre",
                column: "ReaderId",
                principalTable: "Reader",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction); //changed to NoAction from Cascade
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Reader_ReaderId",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Genre_ReaderId",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "ReaderId",
                table: "Genre");
        }
    }
}
