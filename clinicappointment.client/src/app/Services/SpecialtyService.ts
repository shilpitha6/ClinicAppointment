import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Specialty } from '../Models/specialty.model';

@Injectable({ providedIn: 'root' })
export class SpecialtyService {
  private readonly apiUrl = '/api/specialties';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Specialty[]> {
    return this.http.get<Specialty[]>(this.apiUrl);
  }
}
