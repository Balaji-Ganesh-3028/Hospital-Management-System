export interface PatientRequest {
  userId: number;
  doj: string;
  bloodGroup: number | string;
  allergies: string;
  chronicDiseases: string;
  emergencyContactName: string;
  emergencyContactNumber: string;
  insuranceProvider: string;
  insuranceNumber: string;
  MedicalHistoryNotes: string;
  createdAt?: string;
  updatedAt?: string;
  createdBy?: string;
  updatedBy?: string;
}

export interface PatientDetails {
  email?: string;
  id?: number;
  userName?: string;
  firstName?: string;
  lastName?: string;
  gender?: number;
  age?: number;
  phoneNumber?: string;
  bloodGroup?: number | string;
  bloodGroupName?: string;
  patientId?: number;
  doj?: string;
  allergies?: string;
  chronicDiseases?: string;
  emergencyContactName?: string;
  emergencyContactNumber?: string;
  insuranceProvider?: string;
  insuranceNumber?: string;
  medicalHistoryNotes?: string;
}
