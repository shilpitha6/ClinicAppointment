import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { doctor } from '../../Models/doctor.model';
import { Specialty } from '../../Models/specialty.model';
import { DoctorService } from '../../Services/doctorService';
import { SpecialtyService } from '../../Services/SpecialtyService';
import { BookingStateService } from '../../Services/BookingstateService';


@Component({
  selector: 'app-doctor-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './doctor-list.html',
  styleUrl: './doctor-list.css'
})
export class DoctorList implements OnInit {
  specialties = signal<Specialty[]>([]);
  doctors = signal<doctor[]>([]);
 

  constructor(
    private specialtyService: SpecialtyService,
    private doctorService: DoctorService,
    private bookingState: BookingStateService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.specialtyService.getAll().subscribe({
      next: data => this.specialties.set(data)
    
    });
    this.loadDoctors();
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

  selectDoctor(doctor: doctor): void {
    this.bookingState.setDoctor(doctor);
    this.router.navigate(['/booking/slots']);
  }

  private loadDoctors(specialtyId?: number): void {
    
    this.doctorService.getDoctors(specialtyId).subscribe({
      next: data => {
        this.doctors.set(data);
        
      },
      error: () => {
       
        this.doctors.set([]);
       
      }
    });
  }
}
