import type { DoctorDetails, DoctorRequest } from "../../../Models/Doctor";

export const setAdaptor = (data: DoctorDetails): DoctorRequest => {
  return {
    userId: toGetUserId(),
    dateOfAssociation: data.dateOfAssociation || "",
    licenseNumber: data.licenseNumber || "",
    qualification: data.qualification || 0,
    specialisation: data.specialisation || 0,
    designation: data.designation || 0,
    experienceYears: data.experienceYears || 0,
  };
};

const toGetUserId = () => {
  const data = localStorage.getItem("user");
  console.log(data);
  if (data) return JSON.parse(data).userId;
  else return 0;
};
