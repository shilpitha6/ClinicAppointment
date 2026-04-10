import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { AvailabilitySlot } from '../../Models/availability-slot.model';
import { SlotService } from '../../Services/SlotsService';
import { DoctorService } from '../../Services/doctorService';
import { doctor } from '../../Models/doctor.model';

@Component({
  selector: 'app-slot-availability',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './slots-availability.html',
  styleUrl: './slots-availability.css'
})
export class SlotAvailability implements OnInit {

  slots = signal<AvailabilitySlot[]>([]);
  doctor = signal<doctor | null>(null); 
  doctorId!: number;

  constructor(
    private slotService: SlotService,
    private doctorService: DoctorService, 
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.doctorId = Number(this.route.snapshot.paramMap.get('doctorId'));

    if (!this.doctorId) {
      this.router.navigate(['/doctors']);
      return;
    }

  
    this.doctorService.getDoctorById(this.doctorId).subscribe({
      next: data => this.doctor.set(data)
    });

  
    this.slotService.getSlotsByDoctor(this.doctorId).subscribe({
      next: data => this.slots.set(data)
    });
  }

  selectSlot(slot: AvailabilitySlot): void {
    this.router.navigate(['/booking/confirm', this.doctorId, slot.slot_id]);
  }

  goBack(): void {
    this.router.navigate(['/doctors']);
  }
}
