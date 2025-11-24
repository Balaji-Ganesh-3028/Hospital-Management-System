import type { AppointmentDetails, AppointmentRequest } from "../../Models/Appointment";
import API from "../API/api";

export const GetAllAppointments = () => {
  return API.get("api/Appointment", { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } });
};

export const GetAppointmentDetails = async (id: number): Promise<AppointmentDetails> => {
  const response = await API.get(`api/Appointment/${id}`, { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } });
  return response.data;

};

/**
 * Inserts a new appointment into the database.
 * @param {AppointmentRequest} data - The appointment details to be inserted.
 * @returns {Promise<Response>} - A promise that resolves to the response from the API.
 */
export const InsertAppointment = (data: AppointmentRequest) => {
  return API.post("api/Appointment", data, { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } });
};

/**
 * Updates an existing appointment in the database.
 * @param {AppointmentRequest} data - The appointment details to be updated.
 * @returns {Promise<Response>} - A promise that resolves to the response from the API.
 */
export const UpdateAppointment = (data: AppointmentRequest) => {
  return API.put("api/Appointment", data, { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } });
};