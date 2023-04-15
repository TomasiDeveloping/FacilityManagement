using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMaintenance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cf67ce75-2fad-419d-be1a-7ed610fc860c"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Maintainances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextExecution",
                table: "Maintainances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("5916fadc-3024-491a-82d1-18e0dce33e74"), null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("529ba03e-574a-4332-8775-a4e0d8f26ed4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aaf4ca60-42fe-43ec-867f-3a2c71e4ece6", "AQAAAAIAAYagAAAAEBTmVJD9oBsoxCZFj28CBKQYCKn1sUWsiJf/TizNRGaGmTi/JCxGxa9iG2sAeGGK3A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5916fadc-3024-491a-82d1-18e0dce33e74"));

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Maintainances");

            migrationBuilder.DropColumn(
                name: "NextExecution",
                table: "Maintainances");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("cf67ce75-2fad-419d-be1a-7ed610fc860c"), null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("529ba03e-574a-4332-8775-a4e0d8f26ed4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6fab3ee5-3fca-49ff-bace-c891c7b62259", "AQAAAAIAAYagAAAAELnrMUTQtP8YNRkZGSCos4JNaGszRY0dTi0Ptj99PWu9QqzKrYaPStn6/ywrAD2+DA==" });
        }
    }
}
