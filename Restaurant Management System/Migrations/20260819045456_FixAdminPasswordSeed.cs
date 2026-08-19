using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class FixAdminPasswordSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEK1cw2qTNWR41LfygMwWJRHTuaeElKrQLaEdV8AIf+aj2B8SSJ2RxUPXIoR9pZIPaA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAEAAYagAAAAEIBBFlObCcUT4GE73GkFH4lWq6e0LaQfKhXlZhcVpAETHMIdgL6VySj+97d6wbntkQ==");
        }
    }
}
