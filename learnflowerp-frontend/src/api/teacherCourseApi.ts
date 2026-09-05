import api from "./axios";

export const assignTeacherCourseApi = async (employeeId: number, courseId: number) => {
  const response = await api.post("/teacher-courses/assign", { employeeId, courseId });
  return response.data;
};

export const unassignTeacherCourseApi = async (employeeId: number, courseId: number) => {
  const response = await api.delete("/teacher-courses/unassign", { data: { employeeId, courseId } });
  return response.data;
};

export const getTeacherCoursesApi = async (employeeId: number) => {
  const response = await api.get(`/teacher-courses/teacher/${employeeId}`);
  return response.data;
};

export const getCourseTeachersApi = async (courseId: number) => {
  const response = await api.get(`/teacher-courses/course/${courseId}`);
  return response.data;
};
