import { Component, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Router } from '@angular/router';
import { Appointment } from '../../Models/appointment.model';
import { AppointmentService } from '../../Services/AppointmentService';
import { AuthService } from '../../Services/AuthService';

@Component({
  selector: 'app-patient-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './patient-dashboard.html',
  styleUrl: './patient-dashboard.css'
})
export class PatientDashboard implements OnInit {
  appointments = signal<Appointment[]>([]);
  loading = signal(false);
  expandedId = signal<number | null>(null);

  upcoming = computed(() =>
    this.appointments().filter(a =>
      a.status === 'Pending' || a.status === 'Confirmed'
    )
  );

  past = computed(() =>
    this.appointments().filter(a =>
      a.status === 'Completed' || a.status === 'Cancelled'
    )
  );

  constructor(
    private appointmentService: AppointmentService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const patient = this.authService.currentPatient();
    if (!patient) {
      this.router.navigate(['/login']);
      return;
    }

    this.loading.set(true);
    this.appointmentService.getByPatient(patient.patient_id).subscribe({
      next: data => {
        this.appointments.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  toggleHistory(id: number): void {
    this.expandedId.set(this.expandedId() === id ? null : id);
  }

  cancelAppointment(appointmentId: number): void {
    const patient = this.authService.currentPatient();
    this.appointmentService.updateStatus(appointmentId, {
      new_status: 'Cancelled',
      changed_by: patient?.username ?? 'Patient',
      reason: 'Cancelled by patient'
    }).subscribe({
      next: () => this.ngOnInit()
    });
  }

  getStatusClass(status: string): string {
    const map: Record<string, string> = {
      Pending: 'badge-pending',
      Confirmed: 'badge-confirmed',
      Cancelled: 'badge-cancelled',
      Completed: 'badge-completed'
    };
    return map[status] ?? '';
  }
}
