export interface StatusHistory {
  status_id: number;
  appointment_id: number;
  previous_status: string | null;
  updated_status: string;
  changed_by: string;
  changed_at: string;
  reason: string;
}
