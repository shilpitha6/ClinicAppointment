import { Specialty } from "./specialty.model";


export interface doctor {
  doctor_id: number;
  first_name: string;
  last_name: string;
  specialty_id: number;
  specialty: Specialty ;
 
}
