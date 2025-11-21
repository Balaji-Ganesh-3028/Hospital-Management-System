export interface DoctorRequest {
  userId?: number;
  dateOfAssociation: string;
  licenseNumber: string;
  qualification: number;
  specialization: number;
  designation: number;
  experience: number;
  createdAt?: string;
  updatedAt?: string;
  createdBy?: string;
  updatedBy?: string;
}

export interface DoctorDetails {
  email?: string;
  id?: number;
  userName?: string;
  firstName?: string;
  lastName?: string;
  gender?: number;
  age?: number;
  phoneNumber?: string;
  doctorId?: number;
  dateOfAssociation?: string;
  licenseNumber?: string;
  qualification?: number;
  qualificationName?: string;
  specialization?: number;
  specializationName?: string;
  designation?: number;
  designationName?: string;
  experience?: number;
}
