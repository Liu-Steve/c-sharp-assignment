using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusHelper.Migrations
{
    public partial class modify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "recordId",
                table: "DangerIndex",
                newName: "RecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "DangerIndex",
                newName: "recordId");
        }
    }
}
