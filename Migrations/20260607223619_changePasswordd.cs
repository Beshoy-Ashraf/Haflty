using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Haflty.Migrations
{
    /// <inheritdoc />
    public partial class changePasswordd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "HashPassword");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashPassword",
                table: "Users",
                newName: "Password");
        }
    }
}
