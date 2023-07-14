using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedReaderMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Reader");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Reader",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
