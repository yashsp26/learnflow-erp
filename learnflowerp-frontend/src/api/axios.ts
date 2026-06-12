import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");

  if (token) {
    config.headers.Authorization =
      `Bearer ${token}`;
  }

  return config;
});

api.interceptors.response.use(
  (response) => response,

  async (error) => {
    const originalRequest = error.config;

    if (
      error.response?.status === 401 &&
      !originalRequest._retry
    ) {
      originalRequest._retry = true;

      const refreshToken =
        localStorage.getItem("refreshToken");

      if (!refreshToken) {
        localStorage.clear();
        window.location.href = "/login";
        return Promise.reject(error);
      }

      try {
        const refreshResponse =
          await axios.post(
            `${import.meta.env.VITE_API_BASE_URL}/auth/refresh-token`,
            {
              refreshToken,
            }
          );

        const newToken =
          refreshResponse.data.token;

        const newRefreshToken =
          refreshResponse.data.refreshToken;

        localStorage.setItem(
          "token",
          newToken
        );

        localStorage.setItem(
          "refreshToken",
          newRefreshToken
        );

        originalRequest.headers.Authorization =
          `Bearer ${newToken}`;

        return api(originalRequest);
      } catch {
        localStorage.clear();

        window.location.href = "/login";

        return Promise.reject(error);
      }
    }

    return Promise.reject(error);
  }
);

export default api;