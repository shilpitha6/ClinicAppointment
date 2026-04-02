import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Appointment } from '../Models/appointment.model';
import { Patient } from '../Models/patient.model';

@Injectable({ providedIn: 'root' })
export class PatientDashboardService {
  private readonly apiUrl = '/api/patients';

  constructor(private http: HttpClient) { }

  getPatientDetails(patientId: number): Observable<Patient> {
    return this.http.get<Patient>(`${this.apiUrl}/${patientId}`);
  }

  getPatientAppointment(patientId: number): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(`${this.apiUrl}/${patientId}/appointments`);
  }


 
}
