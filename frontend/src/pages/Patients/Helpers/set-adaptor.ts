import type { PatientDetails, PatientRequest } from "../../../Models/Patient";

export const setAdaptor = (
  data: PatientDetails,
  id: number
): PatientRequest => {
  return {
    userId: id,
    doj: data.doj || "",
    bloodGroup: data.bloodGroup || "",
    allergies: data.allergies || "",
    chronicDiseases: data.chronicDiseases || "",
    emergencyContactName: data.emergencyContactName || "",
    emergencyContactNumber: data.emergencyContactNumber || "",
    insuranceProvider: data.insuranceProvider || "",
    insuranceNumber: data.insuranceNumber || "",
    MedicalHistoryNotes: data.medicalHistoryNotes || "",
  };
};

export const toGetUserId = () => {
  const data = localStorage.getItem("user");
  console.log(data);
  if (data) return JSON.parse(data).userId;
  else return 0;
};
