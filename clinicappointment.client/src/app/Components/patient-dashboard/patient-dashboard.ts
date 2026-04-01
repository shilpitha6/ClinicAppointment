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
  
  constructor(
    private appointmentService: AppointmentService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const patient = this.authService.currentPatient();
    if (!patient) {
      this.router.navigate(['/dashboard']);
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




 
}
