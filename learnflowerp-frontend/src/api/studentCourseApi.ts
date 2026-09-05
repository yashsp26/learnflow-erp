import api from "./axios";

export const assignStudentCourseApi = async (studentId: number, courseId: number) => {
  const response = await api.post("/student-courses/assign", { studentId, courseId });
  return response.data;
};

export const removeStudentCourseApi = async (studentId: number, courseId: number) => {
  const response = await api.delete("/student-courses/remove", { data: { studentId, courseId } });
  return response.data;
};

export const getStudentCoursesApi = async (studentId: number) => {
  const response = await api.get(`/student-courses/student/${studentId}`);
  return response.data;
};

export const getCourseStudentsApi = async (courseId: number) => {
  const response = await api.get(`/student-courses/course/${courseId}`);
  return response.data;
};
