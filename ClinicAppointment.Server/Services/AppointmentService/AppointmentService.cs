using ClinicAppointment.Server.Services.AppointmentService;
using ClinicAppointmentProject.DTO;
using ClinicAppointmentProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointment.Server.Services.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ClinicAppointmentDatabaseContext _context;

        public AppointmentService(ClinicAppointmentDatabaseContext context)
        {
            _context = context;
        }

        public async Task<AppointmentDTO> BookAppointmentAsync(CreateAppointmentDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                
                bool isSlotTaken = await _context.Appointment
                    .AnyAsync(a => a.slot_id == dto.slot_id
                               && a.status != "Cancelled");

                if (isSlotTaken)
                    throw new Exception("Slot is already booked. Please choose another slot.");

                
                bool slotExists = await _context.AvailabilitySlots
                    .AnyAsync(s => s.slot_id == dto.slot_id
                               && s.doctor_id == dto.doctor_id);

                if (!slotExists)
                    throw new Exception("Slot does not belong to this doctor.");

             
                var appointment = new Appointment
                {
                    slot_id = dto.slot_id,
                    doctor_id = dto.doctor_id,
                    patient_id = dto.patient_id,
                    status = "Pending",
                    created_at = DateOnly.FromDateTime(DateTime.Now)
                };

                _context.Appointment.Add(appointment);
                await _context.SaveChangesAsync();

           
                var slot = await _context.AvailabilitySlots
                    .FirstOrDefaultAsync(s => s.slot_id == dto.slot_id);

                if (slot != null)
                {
                    slot.is_booked = true;
                    await _context.SaveChangesAsync();
                }

              
                //_context.StatusHistory.Add(new StatusHistory
                //{
                //    appointment_id = appointment.appointment_id,
                //    previous_status = null,
                //    updated_status = "Pending",
                //    changed_by = "Patient",
                //    changed_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                //    reason = "Appointment booked"
                //});

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new AppointmentDTO
                {
                    appointment_id = appointment.appointment_id,
                    slot_id = appointment.slot_id,
                    doctor_id = appointment.doctor_id,
                    patient_id = appointment.patient_id,
                    status = appointment.status,
                    created_at = appointment.created_at
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<string> UpdateStatusAsync(int appointmentId, UpdateStatusDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var appointment = await _context.Appointment
                    .FirstOrDefaultAsync(a => a.appointment_id == appointmentId);

                if (appointment == null)
                    throw new Exception("Appointment not found.");

                string previousStatus = appointment.status!;

             
                appointment.status = dto.new_status;
                await _context.SaveChangesAsync();

                
                if (dto.new_status == "Cancelled")
                {
                    var slot = await _context.AvailabilitySlots
                        .FirstOrDefaultAsync(s => s.slot_id == appointment.slot_id);

                    if (slot != null)
                    {
                        slot.is_booked = false;
                        await _context.SaveChangesAsync();
                    }
                }

           
                _context.StatusHistory.Add(new StatusHistory
                {
                    appointment_id = appointmentId,
                    previous_status = previousStatus,
                    updated_status = dto.new_status,
                    changed_by = dto.changed_by,
                    changed_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    reason = dto.reason
                });

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return $"Status updated from '{previousStatus}' to '{dto.new_status}' successfully.";
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}