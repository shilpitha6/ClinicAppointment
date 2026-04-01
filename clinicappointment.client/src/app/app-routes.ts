import { Routes } from '@angular/router';
import { Register } from './Components/register/register';
import { DoctorList } from './Components/doctor-list/doctor-list';
import { SlotAvailability } from './Components/slots-availability/slots-availability';
import { BookingConfirm } from './Components/booking-confirm/booking-confirm';
import { PatientDashboard } from './Components/patient-dashboard/patient-dashboard';
import { Login } from './Components/login/login';

export const routes: Routes = [
  { path: 'register', component: Register },
  { path: 'doctors', component: DoctorList },
  { path: 'booking/slots', component: SlotAvailability },
  { path: 'booking/confirm', component: BookingConfirm },
  { path: 'dashboard', component: PatientDashboard },
  { path: 'login', component: Login }
  
];
