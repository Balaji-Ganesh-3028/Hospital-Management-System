import type { Lookup } from "../../Models/Lookups";
import type { AllUserDetails, UserProfile, UserProfileFormData } from "../../Models/User-Profile";
import API from "../API/api";

/**
 * Inserts a new user profile into the database.
 * @param data The user profile data to be inserted
 * @returns A promise that resolves to a string containing the result of the operation
 */
export const InsertUserProfile = async (data: UserProfile): Promise<string> =>{
  const response = await API.post<string>("api/User/profile", data)
  return response?.data
}

/**
 * Retrieves a list of gender lookup values from the database.
 * @returns A promise that resolves to an array of Lookup objects containing the gender lookup values.
 */
export const GetGender = async (): Promise<Lookup[]> => {
  const response = await API.get<Lookup[]>("api/Lookups/gender");
  return response.data
}

export const GetAllUserProfile = async (): Promise<AllUserDetails[]> => {
  const response = await API.get<AllUserDetails[]>("api/User/get-all-users");
  return response.data
}

export const GetUserProfile = async (userId: number): Promise<UserProfileFormData> => {
  const response = await API.get<UserProfileFormData>(`api/User/get-user?userId=${userId}`);
  return response.data
}

export const DeleteUesrProfile = async (userId: number): Promise<string> => {
  const response = await API.delete<string>(`api/User/delete-user?userId=${userId}`);
  return response.data
}