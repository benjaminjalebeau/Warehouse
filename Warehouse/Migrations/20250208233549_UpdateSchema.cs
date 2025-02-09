using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Workers",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Workers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Workers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Customers",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Customers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Customers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Customers",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Workers",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Workers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Workers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Customers",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Customers",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Customers",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Customers",
                newName: "email");
        }
    }
}
