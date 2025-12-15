using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chatbot.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBotUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "c7bae062-b8cb-417e-8cba-2c4d28d42447", "Bot", "Bot@chatbot.do", true, false, null, null, null, "AQAAAAIAAYagAAAAECArrU6jVqv7SvTsCF3KP9lxs6mS2YDraNm6GBg6l7FPqU3037n2+Lr0s2n+nfFl9g==", null, false, null, false, "Bot@chatbot.do" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
