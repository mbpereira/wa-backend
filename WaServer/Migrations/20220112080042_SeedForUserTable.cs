using Microsoft.EntityFrameworkCore.Migrations;

namespace WaServer.Migrations
{
    public partial class SeedForUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "IdUser", "Name", "Email", "Password" },
                values: new object[] { 1, "Admin", "admin@sudo.su", BCrypt.Net.BCrypt.HashPassword("40028922") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "IdUser",
                keyValue: 1);
        }
    }
}
