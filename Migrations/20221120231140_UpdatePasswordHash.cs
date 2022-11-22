using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todolistapplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "userInfos");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "TodoItems",
                newName: "Status");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "userInfos",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "userInfos");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TodoItems",
                newName: "status");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "userInfos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
