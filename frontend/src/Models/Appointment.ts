export interface AppointmentRequest {
  id?: number;
  appointmentDate: string;
  patientId: number;
  doctorId: number;
  purposeOfVisit: number;
  illnessOrDisease: string;
  proceduresOrMedication: string;
  currentStatus: number;
  createdAt?: string;
  updatedAt?: string;
  createdBy?: string;
  updatedBy?: string;
}

export interface AppointmentDetails {
  appointmentId: number;
  appointmentDate: string;
  purposeOfVisit: number;
  purposeOfVisitName: string;
  illnessOrDisease: string;
  proceduresOrMedication: null;
  currentStatus: number;
  status: string;
  createdAt: null;
  updatedAt: null;
  createdBy: null;
  updatedBy: null;
  patientId: number;
  patientFirstName: string;
  patientLastName: string;
  patientAge: null;
  patientGender: null;
  patientPhoneNumber: null;
  patientAddressLine1: null;
  patientAddressLine2: null;
  patientCity: null;
  patientState: null;
  patientCountry: null;
  patientBloodGroup: null;
  doctorId: number;
  doctorFirstName: string;
  doctorLastName: string;
  specialization: null;
  designation: null;
  experience: null;
  doctorPhoneNumber: null;
  doctorAddressLine1: null;
  doctorCity: null;
  doctorState: null;
  doctorCountry: null;
}
