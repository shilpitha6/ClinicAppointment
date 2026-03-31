import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { doctor } from '../Models/doctor.model';

@Injectable({ providedIn: 'root' })
export class DoctorService {
  private readonly apiUrl = '/api/doctors';

  constructor(private http: HttpClient) { }

  getDoctors(specialtyId?: number): Observable<doctor[]> {
    let params = new HttpParams();
    
    if (specialtyId !== undefined && specialtyId !== null && !isNaN(specialtyId)) {
      params = params.set('specialtyId', specialtyId.toString());
    }
    return this.http.get<doctor[]>(this.apiUrl, { params });
  }

  getDoctorById(doctorId: number): Observable<doctor> {
    return this.http.get<doctor>(`${this.apiUrl}/${doctorId}`);
  }
}
