import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AvailabilitySlot } from '../Models/availability-slot.model';

@Injectable({ providedIn: 'root' })
export class SlotService {
  private readonly apiUrl = '/api/doctors';

  constructor(private http: HttpClient) { }

  getSlotsByDoctor(doctorId: number): Observable<AvailabilitySlot[]> {
    return this.http.get<AvailabilitySlot[]>(`${this.apiUrl}/${doctorId}/slots`);
  }
}
