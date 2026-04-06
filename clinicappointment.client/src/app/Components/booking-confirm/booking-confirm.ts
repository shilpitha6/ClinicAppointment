import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AppointmentService } from '../../Services/AppointmentService';
import { BookingStateService } from '../../Services/BookingstateService';
import { Patient } from '../../Models/patient.model';


@Component({
  selector: 'app-booking-confirm',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './booking-confirm.html',
  styleUrl: './booking-confirm.css'
})
export class BookingConfirm implements OnInit {
  patient = signal<Patient | null>(null);
  loading = signal(false);
  error = signal('');
  submitted = signal(false);
  
  constructor(
    public bookingState: BookingStateService,
    private appointmentService: AppointmentService,
    
    private router: Router
  ) { }

  ngOnInit(): void {
    this.patient.set({ patient_id: 1 } as Patient);

    const doctor = this.bookingState.selectedDoctor();
    const slot = this.bookingState.selectedSlot();

    if (!doctor || !slot) {
      this.bookingState.clear();
      this.router.navigate(['/doctors']);
      return;
    }
  }

  confirmBooking(): void {

    if (this.loading() || this.submitted()) return;
 
    const doctor = this.bookingState.selectedDoctor();
    const slot = this.bookingState.selectedSlot();
    const currentPatient = this.patient();


    if (!doctor || !slot) {
      this.bookingState.clear();
      this.router.navigate(['/doctors']);
      return;
    }

    if (!currentPatient?.patient_id) {
      this.router.navigate(['/dashboard']);
      return;
    }

    this.loading.set(true);
    this.error.set('');


    this.appointmentService.createAppointment({
      slot_id: slot.slot_id,
      doctor_id: doctor.doctor_id,
      patient_id: currentPatient.patient_id
    }).subscribe({
      next: () => {
        this.submitted.set(true);
        this.bookingState.clear();
        this.router.navigate(['/dashboard']);
      },
      error: err => {
       
        this.loading.set(false);
        const msg: string = err.error?.message ?? 'Booking failed. Please try again.'
    this.error.set(msg);

        if (err.status === 400) {
          this.bookingState.clear();
          setTimeout(() => {
            this.error.set('');
            this.router.navigate(['/booking/slots']);
          }, 2000);
        }

      }
    });
  }

  goBack(): void {
    this.router.navigate(['/booking/slots']);
  }
}
