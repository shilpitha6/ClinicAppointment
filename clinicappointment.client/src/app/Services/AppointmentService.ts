import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  Appointment,
  CreateAppointmentRequest,
  UpdateStatusRequest
} from '../Models/appointment.model';

@Injectable({ providedIn: 'root' })
export class AppointmentService {
  private readonly apiUrl = '/api/appointments';
  private readonly patientUrl = '/api/patients';

  constructor(private http: HttpClient) { }

  getByPatient(patientId: number): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(
      `${this.patientUrl}/${patientId}/appointments`
    );
  }


  createAppointment(body: CreateAppointmentRequest): Observable<Appointment> {
    return this.http.post<Appointment>(`${this.apiUrl}`, body);
  }

  CancelAppointment(appointmentId: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${appointmentId}/status`,
      {
        new_status: 'Cancelled',
        changed_by: 'Patient',
        reason: 'Cancelled by patient from dashboard'
});
  }


}
