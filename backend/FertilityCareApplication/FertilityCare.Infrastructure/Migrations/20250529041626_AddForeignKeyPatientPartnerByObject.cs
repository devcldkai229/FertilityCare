using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyPatientPartnerByObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patient_PatientParnerId",
                table: "Patient");

            migrationBuilder.AlterColumn<Guid>(
                name: "PatientParnerId",
                table: "Patient",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PatientParnerId",
                table: "Patient",
                column: "PatientParnerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patient_PatientParnerId",
                table: "Patient");

            migrationBuilder.AlterColumn<Guid>(
                name: "PatientParnerId",
                table: "Patient",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PatientParnerId",
                table: "Patient",
                column: "PatientParnerId",
                unique: true,
                filter: "[PatientParnerId] IS NOT NULL");
        }
    }
}
