using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusHelper.Migrations
{
    public partial class nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DangerActions_RealTimeRecords_RealTimeRecordId",
                table: "DangerActions");

            migrationBuilder.DropForeignKey(
                name: "FK_DangerIndex_RealTimeRecords_RealTimeRecordId",
                table: "DangerIndex");

            migrationBuilder.DropForeignKey(
                name: "FK_DangerRecords_WorkInfos_WorkInfoId",
                table: "DangerRecords");

            migrationBuilder.AlterColumn<string>(
                name: "WorkInfoId",
                table: "DangerRecords",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "RealTimeRecordId",
                table: "DangerIndex",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "RealTimeRecordId",
                table: "DangerActions",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_DangerActions_RealTimeRecords_RealTimeRecordId",
                table: "DangerActions",
                column: "RealTimeRecordId",
                principalTable: "RealTimeRecords",
                principalColumn: "RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_DangerIndex_RealTimeRecords_RealTimeRecordId",
                table: "DangerIndex",
                column: "RealTimeRecordId",
                principalTable: "RealTimeRecords",
                principalColumn: "RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_DangerRecords_WorkInfos_WorkInfoId",
                table: "DangerRecords",
                column: "WorkInfoId",
                principalTable: "WorkInfos",
                principalColumn: "WorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DangerActions_RealTimeRecords_RealTimeRecordId",
                table: "DangerActions");

            migrationBuilder.DropForeignKey(
                name: "FK_DangerIndex_RealTimeRecords_RealTimeRecordId",
                table: "DangerIndex");

            migrationBuilder.DropForeignKey(
                name: "FK_DangerRecords_WorkInfos_WorkInfoId",
                table: "DangerRecords");

            migrationBuilder.UpdateData(
                table: "DangerRecords",
                keyColumn: "WorkInfoId",
                keyValue: null,
                column: "WorkInfoId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "WorkInfoId",
                table: "DangerRecords",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "DangerIndex",
                keyColumn: "RealTimeRecordId",
                keyValue: null,
                column: "RealTimeRecordId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "RealTimeRecordId",
                table: "DangerIndex",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "DangerActions",
                keyColumn: "RealTimeRecordId",
                keyValue: null,
                column: "RealTimeRecordId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "RealTimeRecordId",
                table: "DangerActions",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_DangerActions_RealTimeRecords_RealTimeRecordId",
                table: "DangerActions",
                column: "RealTimeRecordId",
                principalTable: "RealTimeRecords",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DangerIndex_RealTimeRecords_RealTimeRecordId",
                table: "DangerIndex",
                column: "RealTimeRecordId",
                principalTable: "RealTimeRecords",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DangerRecords_WorkInfos_WorkInfoId",
                table: "DangerRecords",
                column: "WorkInfoId",
                principalTable: "WorkInfos",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
