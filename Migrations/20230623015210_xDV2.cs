using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanProject.Migrations
{
    /// <inheritdoc />
    public partial class xDV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Tag");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Tag",
                type: "TEXT",
                nullable: true);
        }
    }
}
