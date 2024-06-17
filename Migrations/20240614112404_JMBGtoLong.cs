using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvonaZadatak.Migrations
{
    /// <inheritdoc />
    public partial class JMBGtoLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropUniqueConstraint(
            name: "AK_Users_JMBG",
            table: "Users");

            migrationBuilder.AlterColumn<long>(
                name: "JMBG",
                table: "Users",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddUniqueConstraint(
            name: "AK_Users_JMBG",
            table: "Users",
            column: "JMBG");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "JMBG",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
