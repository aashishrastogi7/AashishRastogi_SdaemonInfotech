using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRUD_Operation.Migrations
{
    /// <inheritdoc />
    public partial class ScriptAddingMasterDataForMoreTesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("e2a4b745-6cfc-4a3c-be5f-a7d91b979e30"));

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "DueDate", "IsComplete", "Title" },
                values: new object[,]
                {
                    { new Guid("18baeb8a-362d-4576-8da2-d836d5abe2c4"), "Testing the data insertion on time of Creation i.e Master data", new DateTime(2025, 5, 18, 13, 2, 40, 887, DateTimeKind.Local).AddTicks(2852), true, "Test Task" },
                    { new Guid("85222b55-996a-4c1b-9d97-f12fd749244a"), "task 2 for testing getall api", new DateTime(2025, 5, 18, 13, 2, 40, 887, DateTimeKind.Local).AddTicks(2907), true, "Task 2" },
                    { new Guid("c9eae515-631e-4680-89c0-6ad5d28c423e"), "task 3 for testing getbyID api", new DateTime(2025, 5, 18, 13, 2, 40, 887, DateTimeKind.Local).AddTicks(2910), true, "Task3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("18baeb8a-362d-4576-8da2-d836d5abe2c4"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("85222b55-996a-4c1b-9d97-f12fd749244a"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("c9eae515-631e-4680-89c0-6ad5d28c423e"));

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "DueDate", "IsComplete", "Title" },
                values: new object[] { new Guid("e2a4b745-6cfc-4a3c-be5f-a7d91b979e30"), "Testing the data insertion on time of Creation i.e Master data", new DateTime(2025, 5, 18, 10, 18, 52, 862, DateTimeKind.Local).AddTicks(6891), true, "Test Task" });
        }
    }
}
