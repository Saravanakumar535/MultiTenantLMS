using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MultiTenantLMS.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Domain", "Name" },
                values: new object[,]
                {
                    { 1, "tenantA.com", "Tenant A" },
                    { 2, "tenantB.com", "Tenant B" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "TenantId", "Title" },
                values: new object[,]
                {
                    { 1, "Intro to C#", 1, "C# Basics" },
                    { 2, "Learn ASP.NET Core", 1, "ASP.NET Core" },
                    { 3, "Intro to Java", 2, "Java Basics" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Role", "TenantId", "Username" },
                values: new object[,]
                {
                    { 1, "hashedpassA", "Admin", 1, "adminA" },
                    { 2, "hashedpassA", "Student", 1, "studentA" },
                    { 3, "hashedpassB", "Admin", 2, "adminB" },
                    { 4, "hashedpassB", "Student", 2, "studentB" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "CourseId", "EnrolledAt", "Progress", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 8, 16, 19, 40, 32, 930, DateTimeKind.Local).AddTicks(3454), 0.0, 2 },
                    { 2, 3, new DateTime(2025, 8, 16, 19, 40, 32, 930, DateTimeKind.Local).AddTicks(3463), 0.0, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
