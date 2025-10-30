using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sitoAutenticazioneFrau.Data.Migrations
{
    /// <inheritdoc />
    public partial class CartAndLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActionTime",
                table: "UserActionLogs",
                newName: "ActionDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActionDate",
                table: "UserActionLogs",
                newName: "ActionTime");
        }
    }
}
