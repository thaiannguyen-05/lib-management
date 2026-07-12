using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinFormsApp1.Migrations
{
    /// <inheritdoc />
    public partial class FixDynamicSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$qRz/V86H6k7Wc7G3P5H6E.GZ9eM3Hk6fCjG9G9G9G9G9G9G9G9G9G");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$wRk/X87I7l8Xd8H4Q6I7F.HZ0fN4Il7gDkH0H0H0H0H0H0H0H0H0H");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$xSl/Y88J8m9Ye9I5R7J8G.Ia1gO5Jm8hElI1I1I1I1I1I1I1I1I1I");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$EGun6pHMDepcRfwvfFdiHOSTzc9ah3foJWPxkaSQfmXnCFQENgCva");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$owap71wfxInYbPIVgGNt7O9iVrnlueO9p37CSWxj2ldBgXuFagsKK");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$Q7ghBeG/EIKmYmIgKX1QTe8Nfvk50fxMCTDGBGswL6arXO81WHa1W");
        }
    }
}
