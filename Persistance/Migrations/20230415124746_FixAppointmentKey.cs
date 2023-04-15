using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixAppointmentKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1ac73d8d-7489-44e9-8ac8-181faf8e3412"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("7354bb78-4331-40c8-97f0-20aeaec8c905"), null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("529ba03e-574a-4332-8775-a4e0d8f26ed4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "762d64cb-7299-451e-8c4d-b8ed13ff4d83", "AQAAAAIAAYagAAAAEKsDkySvwwQctF8K3c/oGf10fV2A5CDAZk0AR+UhkuTr+9AiefFmgHZrOeqUYamd4g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7354bb78-4331-40c8-97f0-20aeaec8c905"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("1ac73d8d-7489-44e9-8ac8-181faf8e3412"), null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("529ba03e-574a-4332-8775-a4e0d8f26ed4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7277c9af-7b31-4c55-b673-8d2a729fa44e", "AQAAAAIAAYagAAAAEA4JjGcrPFJoAXQjDzqf0h0b0HKYqFnrRNMG3JKQkrcmr5EBqtN1eQQz1uJI9Evm9Q==" });
        }
    }
}
