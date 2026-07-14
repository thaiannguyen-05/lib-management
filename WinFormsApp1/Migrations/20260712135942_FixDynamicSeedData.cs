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
                value: "$2b$11$lhf1zGV3kq/9pE7ZIrdQ8eC.DAA/NDLETCdkCeftgPDLGizL8fMO2");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2b$11$CHb9zP70T2g5ijeKUDXr3.FzZ5SOF0pp6WUnJ81qGeWsM4kEYmZsu");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2b$11$Nh4YOgnZF5sD4.l//qmBpu5aH7sSP/1l/1JEv0A4fyWsgbBBuX5w.");
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
