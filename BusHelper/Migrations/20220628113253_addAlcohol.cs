using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusHelper.Migrations
{
    public partial class addAlcohol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Alcohol",
                table: "DangerIndex",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alcohol",
                table: "DangerIndex");
        }
    }
}
