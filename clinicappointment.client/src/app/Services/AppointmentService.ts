import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  Appointment,
  CreateAppointmentRequest,
    AppointmentStatus
} from '../Models/appointment.model';

@Injectable({ providedIn: 'root' })
export class AppointmentService {
  private readonly apiUrl = '/api/appointments';
  private readonly patientUrl = '/api/patients';

  constructor(private http: HttpClient) { }

  


  createAppointment(body: CreateAppointmentRequest): Observable<Appointment> {
    return this.http.post<Appointment>(`${this.apiUrl}`, body);
  }


    UpdateAppointment(appointmentId: number, new_status: AppointmentStatus, changed_by: string, reason: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/${appointmentId}/status`,
      {
        new_status,
        changed_by,
        reason
      });
  }

}
