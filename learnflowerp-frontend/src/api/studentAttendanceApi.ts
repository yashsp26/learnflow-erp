import api from "./axios";
import type { AttendanceStatus } from "../types/attendance";

export const markStudentAttendanceApi = async (courseId: number, students: { studentId: number; status: AttendanceStatus }[]) => {
  const response = await api.post("/student-attendance/mark", { courseId, students });
  return response.data;
};
export const getStudentAttendanceApi = async (studentId: number) => (await api.get(`/student-attendance/student/${studentId}`)).data;
export const getCourseAttendanceApi = async (courseId: number) => (await api.get(`/student-attendance/course/${courseId}`)).data;
export const getTodayStudentAttendanceApi = async () => (await api.get("/student-attendance/today")).data;
