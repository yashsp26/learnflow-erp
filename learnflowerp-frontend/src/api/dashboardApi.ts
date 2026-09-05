import api from "./axios";

export const getAdminDashboardApi = async () => {
  const response = await api.get("/dashboard/admin");

  return response.data;
};

export const getTeacherDashboardApi = async () => {
  const response = await api.get("/dashboard/teacher");

  return response.data;
};

export const getStudentDashboardApi = async () => {
  const response = await api.get("/dashboard/student");

  return response.data;
};
