import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { doctor } from '../Models/doctor.model';

@Injectable({ providedIn: 'root' })
export class DoctorService {
  private readonly apiUrl = '/api/doctors';

  constructor(private http: HttpClient) { }

  getDoctors(specialty_id?: number): Observable<doctor[]> {
    let params = new HttpParams();
    
    if (specialty_id !== undefined && specialty_id !== null && !isNaN(specialty_id)) {
      params = params.set('specialtyId', specialty_id.toString());
    }
    return this.http.get<doctor[]>(this.apiUrl, { params });
  }

  getDoctorById(doctorId: number): Observable<doctor> {
    return this.http.get<doctor>(`${this.apiUrl}/${doctorId}`);
  }
}
