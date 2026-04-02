import { Component, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Router } from '@angular/router';
import { Appointment } from '../../Models/appointment.model';
import { AppointmentService } from '../../Services/AppointmentService';
import { PatientDashboardService } from '../../Services/PatientDashboardService';
import { Patient } from '../../Models/patient.model';

@Component({
  selector: 'app-patient-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './patient-dashboard.html',
  styleUrl: './patient-dashboard.css'
})
export class PatientDashboard implements OnInit {
  patient = signal<Patient | null>(null);
  appointments = signal<Appointment[]>([]);
  loading = signal(false);
  
  constructor(
    private appointmentService: AppointmentService,
    private patientService: PatientDashboardService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const patientId = 1;

    if (!patientId) {
      this.router.navigate(['/dashboard']);
      return;
    }

    this.loading.set(true);

    this.patientService.getPatientDetails(patientId).subscribe({
      next: data => {
        this.patient.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });


    this.patientService.getPatientAppointment(patientId).subscribe({
      next: data => {
        this.appointments.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  cancelBooking(appointmentId: number): void {
    if (confirm('Are you sure want to cancel appointment')) {
      this.loading.set(true);

      this.appointmentService.CancelAppointment(appointmentId).subscribe({
        next: (res) => {
          console.log('Cancelled:', res.message);
          this.loadAppointments();
        },

        error: (err) => {
          console.error('Cancellation failed:', err);
          this.loading.set(false);
        }

      });
    }
  }

  loadAppointments(): void {
    const patientId = 1;
    this.patientService.getPatientAppointment(patientId).subscribe({
      next: data => {
        this.appointments.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }
  
  getStatusClass(status: string): string {
    const map: Record<string, string> = {
      'Pending': 'cell-pending',
      'Confirmed': 'cell-confirmed',
      'Cancelled': 'cell-cancelled',
      'Completed': 'cell-completed'
    };
    return map[status] ?? '';
  }

 
}
