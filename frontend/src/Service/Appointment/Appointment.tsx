import type { AppointmentRequest } from "../../Models/Appointment";
import API from "../API/api";

export const GetAllAppointments = () => {
  return API.get("api/Appointment");
};

export const GetAppointmentDetails = (id: number) => {
  return API.get(`api/Appointment/${id}`);
};

/**
 * Inserts a new appointment into the database.
 * @param {AppointmentRequest} data - The appointment details to be inserted.
 * @returns {Promise<Response>} - A promise that resolves to the response from the API.
 */
export const InsertAppointment = (data: AppointmentRequest) => {
  return API.post("api/Appointment", data);
};

/**
 * Updates an existing appointment in the database.
 * @param {AppointmentRequest} data - The appointment details to be updated.
 * @returns {Promise<Response>} - A promise that resolves to the response from the API.
 */
export const UpdateAppointment = (data: AppointmentRequest) => {
  return API.put("api/Appointment", data);
};