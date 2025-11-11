export interface DoctorRequest {
  userId?: number;
  dateOfAssociation: string;
  licenseNumber: string;
  qualification: number;
  specialisation: number;
  designation: number;
  experienceYears: number;
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
  specialisation?: number;
  specialisationName?: string;
  designation?: number;
  designationName?: string;
  experienceYears?: number;
}
