import api from "./axios";

export const getEmployeesApi = async (page = 1, pageSize = 10, search = "") => {
  const response = await api.get("/employees", { params: { page, pageSize, search } });
  return response.data;
};

export const getEmployeeByIdApi = async (id: number) => {
  const response = await api.get(`/employees/${id}`);
  return response.data;
};

export const updateEmployeeDocumentApi = async (id: number, url: string) => {
  const response = await api.patch(`/employees/${id}/document`, { url });
  return response.data;
};

export const deleteEmployeeApi = async (id: number) => {
  const response = await api.delete(`/employees/${id}`);
  return response.data;
};

export const getMyEmployeeProfileApi = async () => {
  const response = await api.get("/employees/me");
  return response.data;
};

export const updateMyEmployeeProfileApi = async (firstName: string, lastName: string, department: string) => {
  const response = await api.patch("/employees/me", { firstName, lastName, department });
  return response.data;
};

export const completeEmployeeProfileApi = async (firstName: string, lastName: string, department: string) => {
  const response = await api.post("/employees/complete-profile", { firstName, lastName, department });
  return response.data;
};
