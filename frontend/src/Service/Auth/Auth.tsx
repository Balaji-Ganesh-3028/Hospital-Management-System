import type { LoginPayload, LoginResponse, RegisterPayload } from "../../Models/Auth";
import API from "../API/api";

export const register = async(payload: RegisterPayload) => {
  const response = await API.post<string>("api/register", payload);
  return response.data;
}

export const login = async (payload: LoginPayload) => {
  const response = await API.post<LoginResponse>("/login", payload);
  return response.data;
}