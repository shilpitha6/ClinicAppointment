import { Injectable, signal } from '@angular/core';


import { Patient } from '../Models/patient.model';


@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly STORAGE_KEY = 'clinic_patient';

  currentPatient = signal<Patient | null>(this.loadFromStorage());

  private loadFromStorage(): Patient | null {
    const stored = localStorage.getItem(this.STORAGE_KEY);
    return stored ? JSON.parse(stored) : null;
  }

  setPatient(patient: Patient): void {
    localStorage.setItem(this.STORAGE_KEY, JSON.stringify(patient));
    this.currentPatient.set(patient);
  }

  logout(): void {
    localStorage.removeItem(this.STORAGE_KEY);
    this.currentPatient.set(null);
  }

  isLoggedIn(): boolean {
    return this.currentPatient() !== null;
  }
  

}


