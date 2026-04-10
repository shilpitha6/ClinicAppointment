import { Injectable, signal } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Patient } from '../Models/patient.model';
import { doctor } from '../Models/doctor.model';

@Injectable({ providedIn: 'root' })
export class AuthService {

  private readonly PATIENT_KEY = 'clinic_patient';
  private readonly DOCTOR_KEY = 'clinic_doctor';

  private currentPatientSubject = new BehaviorSubject<Patient | null>(
    this.loadFromStorage<Patient>(this.PATIENT_KEY)         
  );
  currentPatient$ = this.currentPatientSubject.asObservable();

 
  setPatient(patient: Patient | null): void {
    if (patient) {
      localStorage.setItem(this.PATIENT_KEY, JSON.stringify(patient));
    } else {
      localStorage.removeItem(this.PATIENT_KEY);
    }
    this.currentPatientSubject.next(patient);
  }

  getcurrentPatientValue(): Patient | null {
    return this.currentPatientSubject.value;
  }
 

  private loadFromStorage<T>(key: string): T | null {
    const raw = localStorage.getItem(key);
    return raw ? JSON.parse(raw) : null;
  }

  private currentDoctorSubject = new BehaviorSubject<doctor | null>(
    this.loadFromStorage<doctor>(this.DOCTOR_KEY)
  );
  currentDoctor$ = this.currentDoctorSubject.asObservable();

  setDoctor(doc: doctor | null): void {
    if (doc) {
      localStorage.setItem(this.DOCTOR_KEY, JSON.stringify(doc));
    } else {
      localStorage.removeItem(this.DOCTOR_KEY);
    }
    this.currentDoctorSubject.next(doc);
  }

  getCurrentDoctorValue(): doctor | null {
    return this.currentDoctorSubject.value;
  }

  
  

}


