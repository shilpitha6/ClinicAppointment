import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
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
  // to know which patient is logged in to each component

  private currentPatientSubject = new BehaviorSubject<Patient | null>(null);
  currentPatient$ = this.currentPatientSubject.asObservable();

  setPatient(patient: Patient | null): void {
    this.currentPatientSubject.next(patient);
  }


  get currentPatientValue(): Patient | null {
    return this.currentPatientSubject.value;
  }
}
