using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aninja_tags_service.Migrations
{
    public partial class namechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "translated_name",
                table: "animes",
                newName: "translated_title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "translated_title",
                table: "animes",
                newName: "translated_name");
        }
    }
}
