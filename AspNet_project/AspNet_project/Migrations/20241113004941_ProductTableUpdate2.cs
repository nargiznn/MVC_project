using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNet_project.Migrations
{
    public partial class ProductTableUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalesCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesCount",
                table: "Products");
        }
    }
}
