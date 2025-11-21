import type { PatientDetails, PatientRequest } from "../../Models/Patient";
import API from "../API/api";

export const GetAllPatients = async (): Promise<PatientDetails[]> => {
   const response = await API.get<PatientDetails[]>('api/Patient');
  return response.data;
};

export const GetPatient = (id: number) => {
  return API.get(`api/Patient/${id}`, { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } })
};

export const InsertPatient = async (data: PatientRequest): Promise<string> => {
  const response = await API.post('api/Patient', data, { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } })
  return response?.data
}

export const UpdatePatient = async (data: PatientRequest): Promise<string> => {
  const response = await API.put('api/Patient', data, { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } })
  return response?.data
}