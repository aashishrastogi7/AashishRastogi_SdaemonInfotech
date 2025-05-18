using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_Operation.Migrations
{
    /// <inheritdoc />
    public partial class TestingDataOnCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "DueDate", "IsComplete", "Title" },
                values: new object[] { new Guid("e2a4b745-6cfc-4a3c-be5f-a7d91b979e30"), "Testing the data insertion on time of Creation i.e Master data", new DateTime(2025, 5, 18, 10, 18, 52, 862, DateTimeKind.Local).AddTicks(6891), true, "Test Task" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("e2a4b745-6cfc-4a3c-be5f-a7d91b979e30"));
        }
    }
}
