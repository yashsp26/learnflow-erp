import api from "./axios";

export const getDesignationsApi = async () => {
  const response = await api.get(
    "/designations"
  );

  return response.data;
};

export const getDesignationByIdApi =
  async (id: number) => {
    const response = await api.get(
      `/designations/${id}`
    );

    return response.data;
  };

export const createDesignationApi =
  async (name: string) => {
    const response = await api.post(
      "/designations",
      {
        name,
      }
    );

    return response.data;
  };

export const updateDesignationApi =
  async (
    id: number,
    name: string
  ) => {
    const response = await api.patch(
      `/designations/${id}`,
      {
        name,
      }
    );

    return response.data;
  };

export const deleteDesignationApi =
  async (id: number) => {
    const response = await api.delete(
      `/designations/${id}`
    );

    return response.data;
  };