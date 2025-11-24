import type { Lookup, Roles } from "../../Models/Lookups";
import API from "../API/api";

export async function GetRoles(): Promise<Roles[]> {
  const response = await API.get<Roles[]>('api/Lookups/Roles');
  return response.data;
}


/**
 * Retrieves a list of gender lookup values from the database.
 * @returns A promise that resolves to an array of Lookup objects containing the gender lookup values.
 */
export const GetGender = async (): Promise<Lookup[]> => {
  const response = await API.get<Lookup[]>("api/Lookups/gender");
  return response.data
}

export const GetBloodGroup = async (): Promise<Lookup[]> => {
  const response = await API.get<Lookup[]>("api/Lookups/blood-group");
  return response.data
}

export const GetAppointmentType = async (): Promise<Lookup[]> => {
  const response = await API.get<Lookup[]>("api/Lookups/appointment-type");
  return response.data
} 

export const GetAppointmentStatus = async (): Promise<Lookup[]> => {
  const response = await API.get<Lookup[]>("api/Lookups/appointment-status");
  return response.data
}

export const GetSpecialisation = async (): Promise<Lookup[]> => {
  const response = await API.get<Lookup[]>("api/Lookups/specialisation");
  return response.data
}

export const GetQualification = async (): Promise<Lookup[]> => {
  const response = await API.get<Lookup[]>("api/Lookups/qualification");
  return response.data
}

export const GetDesignation = async (): Promise<Lookup[]> => {
  const response = await API.get<Lookup[]>("api/Lookups/designation");
  return response.data
}