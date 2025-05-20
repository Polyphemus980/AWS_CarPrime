using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPrime.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureLeaseOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Offers_OfferId",
                table: "Leases");

            migrationBuilder.DropIndex(
                name: "IX_Leases_OfferId",
                table: "Leases");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_OfferId",
                table: "Leases",
                column: "OfferId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Offers_OfferId",
                table: "Leases",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "OfferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Offers_OfferId",
                table: "Leases");

            migrationBuilder.DropIndex(
                name: "IX_Leases_OfferId",
                table: "Leases");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_OfferId",
                table: "Leases",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Offers_OfferId",
                table: "Leases",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "OfferId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
