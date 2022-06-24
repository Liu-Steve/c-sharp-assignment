using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusHelper.Migrations
{
    public partial class modifyTypeInDanger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Temperature",
                table: "DangerIndex",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Yawn",
                table: "DangerActions",
                type: "float",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<float>(
                name: "UsingPhone",
                table: "DangerActions",
                type: "float",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<float>(
                name: "Smoke",
                table: "DangerActions",
                type: "float",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<float>(
                name: "NoSafetyBelt",
                table: "DangerActions",
                type: "float",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<float>(
                name: "LookAround",
                table: "DangerActions",
                type: "float",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<float>(
                name: "LeavingSteering",
                table: "DangerActions",
                type: "float",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<float>(
                name: "Conflict",
                table: "DangerActions",
                type: "float",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<float>(
                name: "CloseEye",
                table: "DangerActions",
                type: "float",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Temperature",
                table: "DangerIndex",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<bool>(
                name: "Yawn",
                table: "DangerActions",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<bool>(
                name: "UsingPhone",
                table: "DangerActions",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<bool>(
                name: "Smoke",
                table: "DangerActions",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<bool>(
                name: "NoSafetyBelt",
                table: "DangerActions",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<bool>(
                name: "LookAround",
                table: "DangerActions",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<bool>(
                name: "LeavingSteering",
                table: "DangerActions",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<bool>(
                name: "Conflict",
                table: "DangerActions",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<bool>(
                name: "CloseEye",
                table: "DangerActions",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }
    }
}
