using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DCWR.Event_Manager.Migrations
{
    public partial class SeedFirstUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "PasswordHash", "UserName" },
                values: new object[] { new Guid("e3212348-a4d3-4bc7-b7c6-d85ae0691547"), "AQAAAAEAACcQAAAAECp0BI0iXa5fkN7/XZm+AHDdyNFskKu0rM8WPOwoKsPq88Mjy6SDRP3wHv0qqWS/+A==", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("e3212348-a4d3-4bc7-b7c6-d85ae0691547"));
        }
    }
}
