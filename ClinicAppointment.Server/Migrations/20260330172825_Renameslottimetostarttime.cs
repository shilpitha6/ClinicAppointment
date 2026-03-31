using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicAppointmentProject.Migrations
{
    /// <inheritdoc />
    public partial class Renameslottimetostarttime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<TimeOnly>(
                name: "start_time",
                table: "AvailabilitySlots",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "pateint_id",
                table: "Appointment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusHistory_appointment_id",
                table: "StatusHistory",
                column: "appointment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_specialty_id",
                table: "Doctor",
                column: "specialty_id");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilitySlots_doctor_id",
                table: "AvailabilitySlots",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_doctor_id",
                table: "Appointment",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_pateint_id",
                table: "Appointment",
                column: "pateint_id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_slot_id",
                table: "Appointment",
                column: "slot_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AvailabilitySlots_slot_id",
                table: "Appointment",
                column: "slot_id",
                principalTable: "AvailabilitySlots",
                principalColumn: "slot_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctor_doctor_id",
                table: "Appointment",
                column: "doctor_id",
                principalTable: "Doctor",
                principalColumn: "doctor_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Patient_pateint_id",
                table: "Appointment",
                column: "pateint_id",
                principalTable: "Patient",
                principalColumn: "patient_id");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailabilitySlots_Doctor_doctor_id",
                table: "AvailabilitySlots",
                column: "doctor_id",
                principalTable: "Doctor",
                principalColumn: "doctor_id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Specialty_specialty_id",
                table: "Doctor",
                column: "specialty_id",
                principalTable: "Specialty",
                principalColumn: "specialty_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusHistory_Appointment_appointment_id",
                table: "StatusHistory",
                column: "appointment_id",
                principalTable: "Appointment",
                principalColumn: "appointment_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AvailabilitySlots_slot_id",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctor_doctor_id",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Patient_pateint_id",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_AvailabilitySlots_Doctor_doctor_id",
                table: "AvailabilitySlots");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Specialty_specialty_id",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusHistory_Appointment_appointment_id",
                table: "StatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_StatusHistory_appointment_id",
                table: "StatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_specialty_id",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_AvailabilitySlots_doctor_id",
                table: "AvailabilitySlots");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_doctor_id",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_pateint_id",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_slot_id",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "start_time",
                table: "AvailabilitySlots");

            migrationBuilder.DropColumn(
                name: "pateint_id",
                table: "Appointment");

           
        }
    }
}
