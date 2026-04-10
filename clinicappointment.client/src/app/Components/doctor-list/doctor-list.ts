import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { doctor } from '../../Models/doctor.model';
import { Specialty } from '../../Models/specialty.model';
import { DoctorService } from '../../Services/doctorService';
import { SpecialtyService } from '../../Services/SpecialtyService';
import { AppointmentService } from '../../Services/AppointmentService';
import { AuthService } from '../../Services/AuthService';
import { Patient } from '../../Models/patient.model';
import { Appointment } from '../../Models/appointment.model';



@Component({
  selector: 'app-doctor-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './doctor-list.html',
  styleUrl: './doctor-list.css'
})
export class DoctorList implements OnInit {

  specialties = signal<Specialty[]>([]);
  doctors = signal<doctor[]>([]);
 
  loading = signal(false);
  showSlotsView = signal(false);
  doctorId!: number;

  prescriptionForms: Record<number, FormGroup> = {};

  constructor(
    private specialtyService: SpecialtyService,
    private doctorService: DoctorService,
    private appointmentService: AppointmentService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.specialtyService.getAll().subscribe({
      next: data => this.specialties.set(data)
    });

    this.loadDoctors();

    const doc = this.authService.getCurrentDoctorValue();
    if (doc) {
      this.doctorId = doc.doctor_id;
      return;
    }
  }
    

  onSpecialtyChange(event: Event): void {
    const value = (event.target as HTMLSelectElement).value;
    if (value === '') {
      this.loadDoctors(undefined);
    } else {
      const parsed = parseInt(value, 10);
      if (!isNaN(parsed)) {
        this.loadDoctors(parsed);
      }
    }
  }

  selectDoctor(d: doctor): void {
    this.router.navigate(['/booking/slots', d.doctor_id]);
  }

  private loadDoctors(specialtyId?: number): void {
    this.doctorService.getDoctors(specialtyId).subscribe({
      next: data => this.doctors.set(data),
      error: () => this.doctors.set([])
    });
  }

  openSlotsView(doc: doctor): void {
    this.doctorId = doc.doctor_id; 
    this.showSlotsView.set(true);
    this.loadConfirmedAppointments();
  }

  closeSlotsView(): void {
    this.showSlotsView.set(false);
 
  }

  toggleSlotsView(): void {
    if (!this.showSlotsView()) {
      this.loadConfirmedAppointments();
    }
    this.showSlotsView.update(v => !v);
  }

  loadConfirmedAppointments(): void {
    this.loading.set(true);
    //console.log("values");

    this.doctorService.getConfirmedAppointments(this.doctorId).subscribe({
      next: (appointmentId) => {
        const confirmed = appointmentId.filter(a => a.status === 'Confirmed');

        console.log("values");
       
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  

  getForm(appointmentId: number): FormGroup {
    return this.prescriptionForms[appointmentId];
  }

 

    
    
 
}
