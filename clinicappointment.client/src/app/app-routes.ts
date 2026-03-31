import { Routes } from '@angular/router';

import { DoctorList } from './Components/doctor-list/doctor-list';
import { SlotAvailability } from './Components/slots-availability/slots-availability';
import { BookingConfirm } from './Components/booking-confirm/booking-confirm';
import { PatientDashboard } from './Components/patient-dashboard/patient-dashboard';

export const routes: Routes = [

  { path: 'doctors', component: DoctorList },
  { path: 'booking/slots', component: SlotAvailability },
  { path: 'booking/confirm', component: BookingConfirm },
  { path: 'dashboard', component: PatientDashboard },
  
];
