export interface Patient {
  patient_id: number;
  first_name: string;
  last_name: string;
  email: string;
  username: string;
}



export interface RegisterPatient {
  first_name: string;
  last_name: string;
  dob: string;
  gender: string;
  email: string;
  address: string;
  username: string;
  password: string;
}

export interface LoginPatient {
  username: string;
  password: string;
}
