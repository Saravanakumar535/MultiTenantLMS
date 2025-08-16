using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiTenantLMS.Migrations
{
    /// <inheritdoc />
    public partial class AddUserEmailSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 1,
                column: "EnrolledAt",
                value: new DateTime(2025, 8, 16, 20, 46, 31, 373, DateTimeKind.Local).AddTicks(2587));

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 2,
                column: "EnrolledAt",
                value: new DateTime(2025, 8, 16, 20, 46, 31, 373, DateTimeKind.Local).AddTicks(2602));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "adminA@tenantA.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: "studentA@tenantA.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Email",
                value: "adminB@tenantB.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Email",
                value: "studentB@tenantB.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 1,
                column: "EnrolledAt",
                value: new DateTime(2025, 8, 16, 19, 40, 32, 930, DateTimeKind.Local).AddTicks(3454));

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 2,
                column: "EnrolledAt",
                value: new DateTime(2025, 8, 16, 19, 40, 32, 930, DateTimeKind.Local).AddTicks(3463));
        }
    }
}
