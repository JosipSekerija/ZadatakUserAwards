using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvonaZadatak.Migrations
{
    /// <inheritdoc />
    public partial class DbIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Awards_Users_DateAwarded",
                table: "Awards_Users",
                column: "DateAwarded");

            migrationBuilder.CreateIndex(
              name: "IX_Awards_Users_User_id",
              table: "Awards_Users",
              column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_Users_Award_id",
                table: "Awards_Users",
                column: "Award_id");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
