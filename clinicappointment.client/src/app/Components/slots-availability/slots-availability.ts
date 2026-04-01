import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AvailabilitySlot } from '../../Models/availability-slot.model';
import { SlotService } from '../../Services/SlotsService';
import { BookingStateService } from '../../Services/BookingstateService';

@Component({
  selector: 'app-slot-availability',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './slots-availability.html',
  styleUrl: './slots-availability.css'
})
export class SlotAvailability implements OnInit {
  slots = signal<AvailabilitySlot[]>([]);
 
  constructor(
    private slotService: SlotService,
    public bookingState: BookingStateService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const doctor = this.bookingState.selectedDoctor();

    if (!doctor) {
      this.router.navigate(['/doctors']);
      return;
    }

    this.slotService.getSlotsByDoctor(doctor.doctor_id).subscribe({
      next: data => {
        this.slots.set(data);
      
      }
    });
  }

  selectSlot(slot: AvailabilitySlot): void {
    this.bookingState.setSlot(slot);
    this.router.navigate(['/booking/confirm']);
  }

  goBack(): void {
    this.router.navigate(['/doctors']);
  }
}
