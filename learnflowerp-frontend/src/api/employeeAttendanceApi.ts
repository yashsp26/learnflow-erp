import api from "./axios";
import type { AttendanceStatus } from "../types/attendance";

export const markEmployeeAttendanceApi = async (employeeId: number, status: AttendanceStatus) => {
  const response = await api.post("/employee-attendance/mark", { employeeId, status });
  return response.data;
};
export const getEmployeeAttendanceApi = async (employeeId: number) => (await api.get(`/employee-attendance/${employeeId}`)).data;
export const getTodayEmployeeAttendanceApi = async () => (await api.get("/employee-attendance/today")).data;
