import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Patient } from '../Models/patient.model';


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

  

}


