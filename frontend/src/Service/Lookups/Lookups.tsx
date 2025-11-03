import type { Roles } from "../../Models/Lookups";
import API from "../API/api";

export async function GetRoles(): Promise<Roles[]> {
  const response = await API.get<Roles[]>('api/Lookups/Roles');
  return response.data;
}