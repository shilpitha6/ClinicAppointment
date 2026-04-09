import { Injectable, signal } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Patient } from '../Models/patient.model';



@Injectable({ providedIn: 'root' })
export class AuthService {

  private readonly STORAGE_KEY = 'clinic_patient';

  private currentPatientSubject = new BehaviorSubject<Patient | null>(
    this.loadFromStorage()           
  );
  currentPatient$ = this.currentPatientSubject.asObservable();

 

  setPatient(patient: Patient | null): void {
    if (patient) {
      localStorage.setItem(this.STORAGE_KEY, JSON.stringify(patient));
    } else {
      localStorage.removeItem(this.STORAGE_KEY);
    }
    this.currentPatientSubject.next(patient);
  }

  getcurrentPatientValue(): Patient | null {
    return this.currentPatientSubject.value;
  }
 

  private loadFromStorage(): Patient | null {
    const raw = localStorage.getItem('clinic_patient');
    return raw ? JSON.parse(raw) : null;
  }

  
  

}


