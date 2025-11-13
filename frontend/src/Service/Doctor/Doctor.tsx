import type { DoctorRequest } from "../../Models/Doctor";
import API from "../API/api";

export const GetAllDoctors = () => {
  return API.get("api/Doctor");
};

export const GetDoctorDetails = (id: number) => {
  return API.get(`api/Doctor/${id}`);
};

export const InsertDoctor = (data: DoctorRequest) => {
  return API.post("api/Doctor", data);
};

export const UpdateDoctor = (data: DoctorRequest) => {
  return API.put("api/Doctor", data);
};