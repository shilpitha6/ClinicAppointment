import { Component, OnInit, signal,OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, forkJoin } from 'rxjs';
import { AppointmentService } from '../../Services/AppointmentService';

import { Patient } from '../../Models/patient.model';
import { PatientDashboardService } from '../../Services/PatientDashboardService';
import { DoctorService } from '../../Services/doctorService';
import { SlotService } from '../../Services/SlotsService';
import { AvailabilitySlot } from '../../Models/availability-slot.model';
import { doctor } from '../../Models/doctor.model';

@Component({
  selector: 'app-booking-confirm',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './booking-confirm.html',
  styleUrl: './booking-confirm.css'
})
export class BookingConfirm implements OnInit {
  doctor = signal<doctor | null>(null);
  slots = signal<AvailabilitySlot[] >([]);
  patient = signal<Patient | null>(null);

  loading = signal(false);
  error = signal('');
  submitted = signal(false);

  private patientSub?: Subscription;

  constructor(
    private route: ActivatedRoute,
    private doctorService: DoctorService,
    private slotService: SlotService,
    private appointmentService: AppointmentService,
    private patientDashboardService: PatientDashboardService,
    private router: Router
  ) { }

  ngOnInit(): void {

    const doctorId = Number(this.route.snapshot.paramMap.get('doctorId'));
    const slotId = Number(this.route.snapshot.paramMap.get('slotId'));

    if (doctorId && slotId) {
      this.loadData(doctorId);
    } else {
      this.router.navigate(['/doctors']);
    }


    this.patientSub = this.patientDashboardService.currentPatient$.subscribe(p => {
      if (p) {
        this.patient.set(p);
      } else {
        this.router.navigate(['/login']);
      }
    });
  }

  loadData(docId: number) {
    this.loading.set(true);

    this.doctorService.getDoctorById(docId).subscribe(data => this.doctor.set(data));

    this.slotService.getSlotsByDoctor(docId).subscribe({
      next: (data: AvailabilitySlot[]) => {
        this.slots.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set("Failed to load availability.");
        this.loading.set(false);
      }
    });
  }

  
  getSelectedSlot(): AvailabilitySlot | undefined {
    const slotId = Number(this.route.snapshot.paramMap.get('slotId'));
    return this.slots().find(s => s.slot_id === slotId);
  }

  confirmBooking(): void {
    const p = this.patient();
    const s = this.getSelectedSlot();

    if (!p || !s) {
      this.error.set("Invalid booking session. Please try again.");
      return;
    }

    this.loading.set(true);
    this.appointmentService.createAppointment({
      slot_id: s.slot_id,
      doctor_id: s.doctor_id,
      patient_id: p.patient_id 
    }).subscribe({
      next: () => {
        this.submitted.set(true);
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.error.set(err.error?.message ?? "Booking failed.");
        this.loading.set(false);
      }
    });
  }

  goBack(): void {
    const doctorId = Number(this.route.snapshot.paramMap.get('doctorId'));
    this.router.navigate(['/booking/slots', doctorId]);
  }

  ngOnDestroy(): void {
    this.patientSub?.unsubscribe();
  }

}





