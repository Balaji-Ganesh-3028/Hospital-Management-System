import type { AllUserDetails, UserProfile, UserProfileFormData, UserProfileQueryParams } from "../../Models/User-Profile";
import API from "../API/api";

/**
 * Inserts a new user profile into the database.
 * @param data The user profile data to be inserted
 * @returns A promise that resolves to a string containing the result of the operation
 */
export const InsertUserProfile = async (data: UserProfile): Promise<string> =>{
  const response = await API.post<string>("api/User", data, { headers: {"Authorization": `Bearer ${localStorage.getItem("token")}`} })
  return response?.data
}

export const UpdateUserProfile = async (data: UserProfile): Promise<string> => {
  console.log(localStorage.getItem("token"));
  const response = await API.put<string>("api/User", data, { headers: {"Authorization": `Bearer ${localStorage.getItem("token")}`} })
  return response?.data
}

export const GetAllUserProfile = async (payload: UserProfileQueryParams): Promise<AllUserDetails[]> => {
  const response = await API.get<AllUserDetails[]>(`api/User`, { headers: {"Authorization": `Bearer ${localStorage.getItem("token")}`}, params: payload });
  return response.data
}

export const GetUserProfile = async (userId: number): Promise<UserProfileFormData> => {
  const response = await API.get<UserProfileFormData>(`api/User/${userId}`, { headers: {"Authorization": `Bearer ${localStorage.getItem("token")}`} });
  return response.data
}

export const DeleteUesrProfile = async (userId: number): Promise<string> => {
  const response = await API.delete<string>(`api/User/${userId}`);
  return response.data
}