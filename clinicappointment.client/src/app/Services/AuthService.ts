import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { Patient, RegisterRequest, LoginRequest } from '../Models/patient.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly apiUrl = '/api/patients';

  currentPatient = signal<Patient | null>(null);

  constructor(private http: HttpClient) {
    
    const saved = localStorage.getItem('patient');
    if (saved) {
      this.currentPatient.set(JSON.parse(saved));
    }
  }

  register(data: RegisterRequest): Observable<Patient> {
    return this.http.post<Patient>(`${this.apiUrl}/register`, data);
  }

  login(data: LoginRequest): Observable<Patient> {
    return this.http.post<Patient>(`${this.apiUrl}/login`, data).pipe(
      tap(patient => {
        this.currentPatient.set(patient);
        localStorage.setItem('patient', JSON.stringify(patient));
      })
    );
  }

  logout(): void {
    this.currentPatient.set(null);
    localStorage.removeItem('patient');
  }

  isLoggedIn(): boolean {
    return this.currentPatient() !== null;
  }
}
