using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPrime.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaseReturnPhotos_LeaseReturns_LeaseReturnId",
                table: "LeaseReturnPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaseReturns_Employees_EmployeeId",
                table: "LeaseReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Customers_LeaserId",
                table: "Leases");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Cars_CarId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Companies_CompanyId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Customers_CustomerId",
                table: "Offers");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseReturnPhotos_LeaseReturns_LeaseReturnId",
                table: "LeaseReturnPhotos",
                column: "LeaseReturnId",
                principalTable: "LeaseReturns",
                principalColumn: "LeaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseReturns_Employees_EmployeeId",
                table: "LeaseReturns",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Customers_LeaserId",
                table: "Leases",
                column: "LeaserId",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Cars_CarId",
                table: "Offers",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Companies_CompanyId",
                table: "Offers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Customers_CustomerId",
                table: "Offers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaseReturnPhotos_LeaseReturns_LeaseReturnId",
                table: "LeaseReturnPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaseReturns_Employees_EmployeeId",
                table: "LeaseReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_Leases_Customers_LeaserId",
                table: "Leases");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Cars_CarId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Companies_CompanyId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Customers_CustomerId",
                table: "Offers");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseReturnPhotos_LeaseReturns_LeaseReturnId",
                table: "LeaseReturnPhotos",
                column: "LeaseReturnId",
                principalTable: "LeaseReturns",
                principalColumn: "LeaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseReturns_Employees_EmployeeId",
                table: "LeaseReturns",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_Customers_LeaserId",
                table: "Leases",
                column: "LeaserId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Cars_CarId",
                table: "Offers",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Companies_CompanyId",
                table: "Offers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Customers_CustomerId",
                table: "Offers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
