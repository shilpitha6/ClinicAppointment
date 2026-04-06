import { Component, OnInit, OnDestroy, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { Subscription } from 'rxjs';
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
export class PatientDashboard implements OnInit, OnDestroy {
  patient = signal<Patient | null>(null);
  appointments = signal<Appointment[]>([]);
  loading = signal(false);
  sortAsc = signal(false);

  private patientSub?: Subscription;

  constructor(
    private appointmentService: AppointmentService,
    private patientDashboardService: PatientDashboardService,
    private router: Router
  ) { }

  
  upcomingAppointments = computed(() => {
    return this.appointments().filter(a =>
      a.status === 'Pending' || a.status === 'Confirmed'
    );
  });


  pastAppointments = computed(() => {
    let filtered = this.appointments().filter(a =>
      a.status === 'Cancelled' || a.status === 'Completed'
    );


    return filtered.sort((a, b) => {
      const dateA = new Date(a.slot_date).getTime();
      const dateB = new Date(b.slot_date).getTime();
      return this.sortAsc() ? dateA - dateB : dateB - dateA;
    });
  });

  ngOnInit(): void {
    this.patientSub = this.patientDashboardService.currentPatient$.subscribe(p => {
      if (p && p.patient_id) {
        this.fetchFullPatientData(p.patient_id);
      } else {
        
        this.fetchFullPatientData(1);
      }
    });
  }
  fetchFullPatientData(id: number): void {
    this.loading.set(true);


    this.patientDashboardService.getPatientDetails(id).subscribe({
      next: (details) => {
        this.patient.set(details);

        this.loadAppointments(id);
      },
      error: (err) => {
        console.error("Could not fetch patient details", err);
        this.loading.set(false);
      }
    });
  }



  ngOnDestroy(): void {
    this.patientSub?.unsubscribe();
  }

  loadAppointments(patientId: number): void {
    this.loading.set(true);
    this.patientDashboardService.getPatientAppointment(patientId).subscribe({
      next: data => {
        this.appointments.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  confirmAppointment(appointmentId: number): void {
    const currentP = this.patient();
    if (!currentP) return;

    this.loading.set(true);
    this.appointmentService.UpdateAppointment(
      appointmentId,
      'Confirmed',
      'Patient',
      'Patient confirmed the slot'
    ).subscribe({
      next: () => this.loadAppointments(currentP.patient_id),
      error: (err) => {
        console.error("Confirmation failed", err);
        this.loading.set(false);
      }
    });
  }

  completeAppointment(appointmentId: number): void {
    const currentP = this.patient();
    if (!currentP) return;

    this.loading.set(true);
    this.appointmentService.UpdateAppointment(
      appointmentId,
      'Completed',
      'Patient',
      'Check up Completed'
    ).subscribe({
      next: () => this.loadAppointments(currentP.patient_id),
      error: (err) => {
        console.error("Complete failed", err);
        this.loading.set(false);
      }
    });
  }



  cancelAppointment(appointmentId: number): void {
    const currentP = this.patient();
    if (!currentP) return;

    if (confirm('Are you sure you want to cancel this appointment?')) {
      this.loading.set(true);
      this.appointmentService.UpdateAppointment(appointmentId, 'Cancelled', 'Patient', 'User cancelled').subscribe({
        next: () => this.loadAppointments(currentP.patient_id),
        error: () => this.loading.set(false)
      });
    }
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
