import type { AppointmentRequest } from "../../Models/Appointment";
import API from "../API/api";

export const GetAllAppointments = () => {
  return API.get("api/Appointment/all-appointments");
};

export const GetAppointmentDetails = (id: number) => {
  return API.get(`api/Appointment/get-appointment?appointmentId=${id}`);
};

export const InsertAppointment = (data: AppointmentRequest) => {
  return API.post("api/Appointment/insert", data);
};