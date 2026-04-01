import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AppointmentService } from '../../Services/AppointmentService';
import { BookingStateService } from '../../Services/BookingstateService';
import { AuthService } from '../../Services/AuthService';

@Component({
  selector: 'app-booking-confirm',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './booking-confirm.html',
  styleUrl: './booking-confirm.css'
})
export class BookingConfirm {
  loading = signal(false);
  error = signal('');

  constructor(
    public bookingState: BookingStateService,
    private appointmentService: AppointmentService,
    private authService: AuthService,
    private router: Router
  ) { }

  confirmBooking(): void {
    const doctor = this.bookingState.selectedDoctor();
    const slot = this.bookingState.selectedSlot();
    const patient = this.authService.currentPatient();

    if (!doctor || !slot) {
      this.router.navigate(['/doctors']);
      return;
    }

    if (!patient) {
      this.router.navigate(['/dashboard']);
      return;
    }

    this.loading.set(true);
    this.error.set('');

    this.appointmentService.createAppointment({
      slot_id: slot.slot_id,
      doctor_id: doctor.doctor_id,
      patient_id: patient.patient_id
    }).subscribe({
      next: () => {
        this.bookingState.clear();
        this.router.navigate(['/dashboard']);
      },
      error: err => {
        this.error.set(err.error?.message ?? 'Booking failed. Please try again.');
        this.loading.set(false);
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/booking/slots']);
  }
}
