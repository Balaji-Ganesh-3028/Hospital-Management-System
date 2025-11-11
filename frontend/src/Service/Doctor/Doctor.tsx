import type { DoctorRequest } from "../../Models/Doctor";
import API from "../API/api";

export const GetAllDoctors = () => {
  return API.get("api/Doctor/all-doctors");
};

export const GetDoctorDetails = (id: number) => {
  return API.get(`api/Doctor/doctor?userId=${id}`);
};

export const InsertDoctor = (data: DoctorRequest) => {
  return API.post("api/Doctor/insert", data);
};