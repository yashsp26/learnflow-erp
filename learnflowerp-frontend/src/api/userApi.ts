import api from "./axios";

export const getUsersApi = async () => {
  const response = await api.get("/users");

  return response.data;
};

export const getUserByIdApi =
  async (id: number) => {
    const response = await api.get(
      `/users/${id}`
    );

    return response.data;
  };

export const createUserApi =
  async (
    username: string,
    roleId: number,
    email: string,
    password: string
  ) => {
    const response = await api.post(
      "/users",
      {
        username,
        roleId,
        email,
        password,
      }
    );

    return response.data;
  };