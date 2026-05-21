import api from "./axios";

export const loginApi = async (
  email: string,
  password: string,
  tenantCode: string
) => {
  const response = await api.post(
    "/auth/login",
    {
      email,
      password,
      tenantCode,
    }
  );

  return response.data;
};

export const forgotPasswordApi =
  async (email: string) => {
    const response = await api.post(
      "/auth/forgot-password",
      {
        email,
      }
    );

    return response.data;
  };

export const resetPasswordApi =
  async (
    email: string,
    otp: string,
    newPassword: string
  ) => {
    const response = await api.post(
      "/auth/reset-password",
      {
        email,
        otp,
        newPassword,
      }
    );

    return response.data;
  };

export const refreshTokenApi =
  async (refreshToken: string) => {
    const response = await api.post(
      "/auth/refresh-token",
      {
        refreshToken,
      }
    );

    return response.data;
  };