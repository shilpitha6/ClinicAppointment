import { StatusHistory } from './status-history.model';

export interface Appointment {
  appointment_id: number;
  patient_id: number;
  doctor_id: number;
  doctor_first_name: string;
  doctor_last_name: string;
  specialty_name: string;
  slot_id: number;
  slot_date: string;
  start_time: string;
  end_time: string;
  status: string;
  status_history: StatusHistory[];
}

export interface CreateAppointmentRequest {
  slot_id: number;
  doctor_id: number;
  patient_id: number;
}

export interface UpdateStatusRequest {
  new_status: string;
  changed_by: string;
  reason: string;
}
