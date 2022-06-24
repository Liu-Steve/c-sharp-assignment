using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusHelper.Migrations
{
    public partial class addModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FacePic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    ManagerId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pwd = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Area = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ManagerId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roads",
                columns: table => new
                {
                    RoadId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoadInfo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roads", x => x.RoadId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    BusId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoadId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.BusId);
                    table.ForeignKey(
                        name: "FK_Buses_Roads_RoadId",
                        column: x => x.RoadId,
                        principalTable: "Roads",
                        principalColumn: "RoadId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RealTimeRecords",
                columns: table => new
                {
                    RecordId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    X = table.Column<double>(type: "double", nullable: false),
                    Y = table.Column<double>(type: "double", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    BusId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RealPic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealTimeRecords", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_RealTimeRecords_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkInfos",
                columns: table => new
                {
                    WorkId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    BusId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ManagerId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DriverId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkInfos", x => x.WorkId);
                    table.ForeignKey(
                        name: "FK_WorkInfos_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkInfos_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkInfos_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "ManagerId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DangerActions",
                columns: table => new
                {
                    RecordId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Smoke = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Yawn = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    NoSafetyBelt = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LeavingSteering = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CloseEye = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UsingPhone = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LookAround = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Conflict = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RealTimeRecordId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangerActions", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_DangerActions_RealTimeRecords_RealTimeRecordId",
                        column: x => x.RealTimeRecordId,
                        principalTable: "RealTimeRecords",
                        principalColumn: "RecordId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DangerIndex",
                columns: table => new
                {
                    recordId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HeartRate = table.Column<int>(type: "int", nullable: false),
                    HighBloodPressure = table.Column<int>(type: "int", nullable: false),
                    LowBloodPressure = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    BloodOxygen = table.Column<int>(type: "int", nullable: false),
                    RealTimeRecordId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangerIndex", x => x.recordId);
                    table.ForeignKey(
                        name: "FK_DangerIndex_RealTimeRecords_RealTimeRecordId",
                        column: x => x.RealTimeRecordId,
                        principalTable: "RealTimeRecords",
                        principalColumn: "RecordId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Calls",
                columns: table => new
                {
                    CallId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    WorkInfoId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calls", x => x.CallId);
                    table.ForeignKey(
                        name: "FK_Calls_WorkInfos_WorkInfoId",
                        column: x => x.WorkInfoId,
                        principalTable: "WorkInfos",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DangerRecords",
                columns: table => new
                {
                    RecordId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Smoke = table.Column<int>(type: "int", nullable: false),
                    Yawn = table.Column<int>(type: "int", nullable: false),
                    SafetyBelt = table.Column<int>(type: "int", nullable: false),
                    LeavingSteering = table.Column<int>(type: "int", nullable: false),
                    CloseEye = table.Column<int>(type: "int", nullable: false),
                    UsingPhone = table.Column<int>(type: "int", nullable: false),
                    LookAround = table.Column<int>(type: "int", nullable: false),
                    Conflict = table.Column<int>(type: "int", nullable: false),
                    WorkInfoId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangerRecords", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_DangerRecords_WorkInfos_WorkInfoId",
                        column: x => x.WorkInfoId,
                        principalTable: "WorkInfos",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LeavingMsgs",
                columns: table => new
                {
                    MsgId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsRead = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    WorkInfoId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeavingMsgs", x => x.MsgId);
                    table.ForeignKey(
                        name: "FK_LeavingMsgs_WorkInfos_WorkInfoId",
                        column: x => x.WorkInfoId,
                        principalTable: "WorkInfos",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_RoadId",
                table: "Buses",
                column: "RoadId");

            migrationBuilder.CreateIndex(
                name: "IX_Calls_WorkInfoId",
                table: "Calls",
                column: "WorkInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_DangerActions_RealTimeRecordId",
                table: "DangerActions",
                column: "RealTimeRecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DangerIndex_RealTimeRecordId",
                table: "DangerIndex",
                column: "RealTimeRecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DangerRecords_WorkInfoId",
                table: "DangerRecords",
                column: "WorkInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeavingMsgs_WorkInfoId",
                table: "LeavingMsgs",
                column: "WorkInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_RealTimeRecords_BusId",
                table: "RealTimeRecords",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkInfos_BusId",
                table: "WorkInfos",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkInfos_DriverId",
                table: "WorkInfos",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkInfos_ManagerId",
                table: "WorkInfos",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calls");

            migrationBuilder.DropTable(
                name: "DangerActions");

            migrationBuilder.DropTable(
                name: "DangerIndex");

            migrationBuilder.DropTable(
                name: "DangerRecords");

            migrationBuilder.DropTable(
                name: "LeavingMsgs");

            migrationBuilder.DropTable(
                name: "RealTimeRecords");

            migrationBuilder.DropTable(
                name: "WorkInfos");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Roads");
        }
    }
}
