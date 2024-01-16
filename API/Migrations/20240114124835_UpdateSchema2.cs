using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorsData_Services_ServiceId",
                table: "DoctorsData");

            migrationBuilder.DropIndex(
                name: "IX_DoctorsData_ServiceId",
                table: "DoctorsData");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "DoctorsData");

            migrationBuilder.AddColumn<int>(
                name: "DoctorDataId",
                table: "Services",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_DoctorDataId",
                table: "Services",
                column: "DoctorDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_DoctorsData_DoctorDataId",
                table: "Services",
                column: "DoctorDataId",
                principalTable: "DoctorsData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_DoctorsData_DoctorDataId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_DoctorDataId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "DoctorDataId",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "DoctorsData",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsData_ServiceId",
                table: "DoctorsData",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorsData_Services_ServiceId",
                table: "DoctorsData",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
