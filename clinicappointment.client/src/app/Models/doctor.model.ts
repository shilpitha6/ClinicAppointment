import { Specialty } from "./specialty.model";


export interface doctor {
  doctorId: number;
  firstName: string;
  lastName: string;
  specialty_id: number;
  specialty: Specialty ;
 
}
