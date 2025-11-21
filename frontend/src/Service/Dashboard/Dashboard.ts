import type { DashboardDetails } from "../../Models/Dashboard";
import API from "../API/api";

export const GetDashboard = async (): Promise<DashboardDetails> => {
  const response = await API.get<DashboardDetails>("api/Dashboard", {
    headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
  });
  return response.data;
};
