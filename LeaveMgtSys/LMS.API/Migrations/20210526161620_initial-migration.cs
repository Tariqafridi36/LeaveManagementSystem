using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.API.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Leaves",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    LeaveReason = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    LogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ContactNo = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "UserId", "CreatedOn", "Email", "IsActive", "Password", "UserName" },
                values: new object[] { 1, new DateTime(2021, 5, 26, 0, 0, 0, 0, DateTimeKind.Local), "peter@gmail.com", true, "Peter@123", "peter@gmail.com" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "CreatedOn", "DepartmentName" },
                values: new object[] { 1, new DateTime(2021, 5, 26, 0, 0, 0, 0, DateTimeKind.Local), "IT" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "ContactNo", "CreatedOn", "DepartmentId", "Designation", "Email", "IsActive", "Name", "Password" },
                values: new object[] { 1, "v-11", "009713454322", new DateTime(2021, 5, 26, 0, 0, 0, 0, DateTimeKind.Local), 1, "Manager", "peter@gmail.com", true, "Peter", null });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.Sql(@"Create PROC GET_LEAVE_DATA
                                    @USER_ID INT
                                    AS
                                    BEGIN
                                    SELECT  
		                                    'Title' as Title, 
		                                    SUM(DATEDIFF(DAY, DATEADD(day, -1, FromDate), DATEADD(day, 1, ToDate))) AS Id,
		                                    LeaveReason,
		                                    GETDATE() as FromDate,
	                                        GETDATE() as ToDate,
		                                    CONVERT(bit, 0) as IsApproved,
		                                    1 as UserId
	                                    FROM Leaves where IsApproved = 1 group by LeaveReason
                                    END", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Leaves");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Departments");
            migrationBuilder.Sql("DROP PROC GET_LEAVE_DATA");
        }
    }
}
