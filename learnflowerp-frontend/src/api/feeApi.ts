import api from "./axios";

export const createFeeApi = async (studentId: number, totalAmount: number, dueDate: string, feeType: string, academicYear: string) => {
  const response = await api.post("/fees", { studentId, totalAmount, dueDate, feeType, academicYear });
  return response.data;
};
export const getFeesApi = async () => (await api.get("/fees")).data;
export const getFeeByIdApi = async (feeId: number) => (await api.get(`/fees/${feeId}`)).data;
export const getStudentFeesApi = async (studentId: number) => (await api.get(`/fees/student/${studentId}`)).data;
