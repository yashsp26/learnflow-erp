import api from "./axios";

export const getCoursesApi = async (
  page = 1,
  pageSize = 10,
  search = ""
) => {
  const response = await api.get("/courses", {
    params: {
      page,
      pageSize,
      search,
    },
  });

  return response.data;
};

export const getCourseByIdApi = async (
  id: number
) => {
  const response = await api.get(
    `/courses/${id}`
  );

  return response.data;
};

export const createCourseApi = async (
  courseCode: string,
  courseName: string,
  credits: number
) => {
  const response = await api.post(
    "/courses",
    {
      courseCode,
      courseName,
      credits,
    }
  );

  return response.data;
};

export const updateCourseApi = async (
  courseId: number,
  courseCode: string,
  courseName: string,
  credits: number
) => {
  const response = await api.patch(
    `/courses/${courseId}`,
    {
      courseCode,
      courseName,
      credits,
    }
  );

  return response.data;
};

export const deleteCourseApi = async (
  courseId: number
) => {
  const response = await api.delete(
    `/courses/${courseId}`
  );

  return response.data;
};