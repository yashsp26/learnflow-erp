import api from "./axios";

import type { Student } from "../types/student";

type ApiResponse<T> = {
  success: boolean;
  data: T;
  message?: string;
  errorCode?: string;
  correlationId?: string;
};

type PaginatedStudents = {
  items: Student[];
  page: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
};

export const getStudentsApi = async (
  page = 1,
  pageSize = 10,
  search = ""
) => {
  const response = await api.get<ApiResponse<PaginatedStudents>>(
    "/students",
    { params: { page, pageSize, search } }
  );

  return response.data;
};

export const getStudentByIdApi = async (id: number) => {
  const response = await api.get<ApiResponse<Student>>(`/students/${id}`);

  return response.data;
};

export const updateStudentDocumentApi = async (id: number, url: string) => {
  const response = await api.patch<ApiResponse<{ updated: boolean }>>(
    `/students/${id}/document`,
    { url }
  );

  return response.data;
};

export const deleteStudentApi = async (id: number) => {
  const response = await api.delete<ApiResponse<{ deleted: boolean }>>(
    `/students/${id}`
  );

  return response.data;
};

export const getMyStudentProfileApi = async () => {
  const response = await api.get("/students/me");
  return response.data;
};

export const updateMyStudentProfileApi = async (firstName: string, lastName: string, dob: string) => {
  const response = await api.patch("/students/me", { firstName, lastName, dob });
  return response.data;
};

export const completeStudentProfileApi = async (firstName: string, lastName: string, dob: string) => {
  const response = await api.post("/students/student-profile", { firstName, lastName, dob });
  return response.data;
};
