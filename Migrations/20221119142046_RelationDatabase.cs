using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todolistapplication.Migrations
{
    /// <inheritdoc />
    public partial class RelationDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_userInfos_userInfoId",
                table: "TodoItems");

            migrationBuilder.RenameColumn(
                name: "userInfoId",
                table: "TodoItems",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TodoItems_userInfoId",
                table: "TodoItems",
                newName: "IX_TodoItems_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_userInfos_UserId",
                table: "TodoItems",
                column: "UserId",
                principalTable: "userInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_userInfos_UserId",
                table: "TodoItems");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TodoItems",
                newName: "userInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_TodoItems_UserId",
                table: "TodoItems",
                newName: "IX_TodoItems_userInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_userInfos_userInfoId",
                table: "TodoItems",
                column: "userInfoId",
                principalTable: "userInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
