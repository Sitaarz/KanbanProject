using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanProject.Migrations
{
    /// <inheritdoc />
    public partial class InitV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Note_NoteId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Tag_TagId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_User_UserId",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Note",
                table: "Note");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Note",
                newName: "Notes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Notes_NoteId",
                table: "Task",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Tags_TagId",
                table: "Task",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Users_UserId",
                table: "Task",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Notes_NoteId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Tags_TagId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Users_UserId",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "Notes",
                newName: "Note");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Note",
                table: "Note",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Note_NoteId",
                table: "Task",
                column: "NoteId",
                principalTable: "Note",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Tag_TagId",
                table: "Task",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_User_UserId",
                table: "Task",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
