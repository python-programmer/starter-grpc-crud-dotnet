using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProtobufCRUDServer.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueToNationalCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_People_NationalCode",
                table: "People",
                column: "NationalCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_NationalCode",
                table: "People");
        }
    }
}
