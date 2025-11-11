import type { PatientDetails, PatientRequest } from "../../Models/Patient";
import API from "../API/api";

export const GetAllPatients = async (): Promise<PatientDetails[]> => {
   const response = await API.get<PatientDetails[]>('api/Patient/all');
  return response.data;
};

export const GetPatient = (id: number) => {
  return API.get(`api/Patient/get-patient?userId=${id}`)
};

export const InsertPatient = async (data: PatientRequest): Promise<string> => {
  const response = await API.post('api/Patient/insert', data)
  return response?.data
}