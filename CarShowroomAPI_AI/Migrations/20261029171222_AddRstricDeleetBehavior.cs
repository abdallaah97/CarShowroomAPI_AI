using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShowroomAPI_AI.Migrations
{
    /// <inheritdoc />
    public partial class AddRstricDeleetBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFeatures_Cars_CarId",
                table: "CarFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_CarFeatures_Features_FeatureId",
                table: "CarFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_CarReviews_Cars_CarId",
                table: "CarReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Branches_BranchId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_ElectricCarProfiles_ElectricProfileId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_FuelCarProfiles_FuelProfileId",
                table: "Cars");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeatures_Cars_CarId",
                table: "CarFeatures",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeatures_Features_FeatureId",
                table: "CarFeatures",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarReviews_Cars_CarId",
                table: "CarReviews",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Branches_BranchId",
                table: "Cars",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_ElectricCarProfiles_ElectricProfileId",
                table: "Cars",
                column: "ElectricProfileId",
                principalTable: "ElectricCarProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_FuelCarProfiles_FuelProfileId",
                table: "Cars",
                column: "FuelProfileId",
                principalTable: "FuelCarProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFeatures_Cars_CarId",
                table: "CarFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_CarFeatures_Features_FeatureId",
                table: "CarFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_CarReviews_Cars_CarId",
                table: "CarReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Branches_BranchId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_ElectricCarProfiles_ElectricProfileId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_FuelCarProfiles_FuelProfileId",
                table: "Cars");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeatures_Cars_CarId",
                table: "CarFeatures",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeatures_Features_FeatureId",
                table: "CarFeatures",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarReviews_Cars_CarId",
                table: "CarReviews",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Branches_BranchId",
                table: "Cars",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_ElectricCarProfiles_ElectricProfileId",
                table: "Cars",
                column: "ElectricProfileId",
                principalTable: "ElectricCarProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_FuelCarProfiles_FuelProfileId",
                table: "Cars",
                column: "FuelProfileId",
                principalTable: "FuelCarProfiles",
                principalColumn: "Id");
        }
    }
}
