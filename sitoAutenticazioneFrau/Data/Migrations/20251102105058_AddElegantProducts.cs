using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sitoAutenticazioneFrau.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddElegantProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 2, "Pantaloni classici neri in tessuto premium", "Pantaloni Eleganti", 79.90m },
                    { 3, "Giacca slim fit in lana, ideale per occasioni formali", "Giacca Sartoriale", 149.00m },
                    { 4, "Abito blu navy in tessuto raffinato con chiusura a due bottoni", "Abito Completo", 199.90m },
                    { 5, "Cintura elegante in vera pelle italiana", "Cintura in Pelle", 39.50m },
                    { 6, "Scarpe stringate eleganti in pelle lucida", "Scarpe Oxford", 129.90m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
