import { Component, OnInit, OnDestroy, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { Appointment } from '../../Models/appointment.model';
import { Patient } from '../../Models/patient.model';
import { doctor } from '../../Models/doctor.model';
import { AppointmentService } from '../../Services/AppointmentService';
import { PatientDashboardService } from '../../Services/PatientDashboardService';
import { DoctorService } from '../../Services/doctorService';

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
  doctorMap = signal<Record<number, doctor>>({});

  loading = signal(false);
  private patientSub?: Subscription;

  upcomingAppointments = computed(() =>
    this.appointments().filter(a => a.status === 'Pending' || a.status === 'Confirmed')
  );

  pastAppointments = computed(() =>
    this.appointments().filter(a => a.status === 'Cancelled' || a.status === 'Completed')
  );

  constructor(
    private appointmentService: AppointmentService,
    private patientDashboardService: PatientDashboardService,
    private doctorService: DoctorService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.patientSub = this.patientDashboardService.currentPatient$.subscribe(p => {
      if (p?.patient_id) {
        this.fetchPatientData(p.patient_id);
      } else {
        this.router.navigate(['/login']);
      }
    });
  }

  fetchPatientData(id: number): void {
    this.loading.set(true);

    this.patientDashboardService.getPatientDetails(id).subscribe(details => {
      this.patient.set(details);

      this.patientDashboardService.getPatientAppointment(id).subscribe({
        next: (data: Appointment[]) => {
          this.appointments.set(data);
          data.forEach(appt => {
            if (!this.doctorMap()[appt.doctor_id]) {
              this.doctorService.getDoctorById(appt.doctor_id)
                .subscribe(doc => {
                  this.doctorMap.update(map => ({
                    ...map,
                    [appt.doctor_id]: doc
                  }));
                });
            }
          });

          this.loading.set(false);
        },
        error: () => this.loading.set(false)
      });
    });
  }

  getDoctorInfo(id: number): doctor | null {
    return this.doctorMap()[id] ?? null;
  }


  confirmAppointment(id: number): void {
    this.loading.set(true);

    this.appointmentService.UpdateAppointment(
      id,
      'Confirmed',
      'Patient',
      'User confirmed appointment'
    ).subscribe({
      next: () => {
 
        this.appointments.update(all =>
          all.map(a =>
            a.appointment_id === id
              ? { ...a, status: 'Confirmed' }
              : a
          )
        );
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }


  cancelAppointment(id: number): void {
    if (!confirm('Are you sure?')) return;

    this.loading.set(true);

    this.appointmentService.UpdateAppointment(
      id,
      'Cancelled',
      'Patient',
      'User action'
    ).subscribe({
      next: () => {
        this.appointments.update(all =>
          all.map(a =>
            a.appointment_id === id
              ? { ...a, status: 'Cancelled' }
              : a
          )
        );
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  getStatusClass(status: string): string {
    const map: Record<string, string> = {
      'Confirmed': 'status-green',
      'Pending': 'status-yellow',
      'Cancelled': 'status-red',
      'Completed': 'status-gray'
    };
    return map[status] ?? 'status-gray';
  }

  ngOnDestroy(): void {
    this.patientSub?.unsubscribe();
  }
}
