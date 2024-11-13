using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNet_project.Migrations
{
    public partial class SliderWordTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "SliderWords",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "SliderWords");
        }
    }
}
