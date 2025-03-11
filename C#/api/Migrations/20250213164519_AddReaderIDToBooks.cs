using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddReaderIDToBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReaderID",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_ReaderID",
                table: "Books",
                column: "ReaderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Readers_ReaderID",
                table: "Books",
                column: "ReaderID",
                principalTable: "Readers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Readers_ReaderID",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ReaderID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ReaderID",
                table: "Books");
        }
    }
}
