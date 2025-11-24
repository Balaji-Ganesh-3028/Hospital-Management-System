import type { DoctorRequest } from "../../Models/Doctor";
import API from "../API/api";

export const GetAllDoctors = () => {
  return API.get("api/Doctor", { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } });
};

export const GetDoctorDetails = (id: number) => {
  console.log("Fetching doctor details for ID:", id, localStorage.getItem("token"));
  return API.get(`api/Doctor/${id}`, { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } });
};

export const InsertDoctor = (data: DoctorRequest) => {
  return API.post("api/Doctor", data, { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } });
};

export const UpdateDoctor = (data: DoctorRequest) => {
  return API.put("api/Doctor", data, { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } });
};