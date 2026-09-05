import axios from "axios";

type ApiEnvelope = {
  success?: boolean;
  data?: unknown;
  message?: string;
  Success?: boolean;
  Data?: unknown;
  Message?: string;
};

const normalizeApiEnvelope = (payload: unknown): unknown => {
  if (!payload || typeof payload !== "object" || Array.isArray(payload)) {
    return payload;
  }

  const envelope = payload as ApiEnvelope;

  if (!("Data" in envelope || "Success" in envelope || "Message" in envelope)) {
    return payload;
  }

  return {
    ...envelope,
    success: envelope.success ?? envelope.Success,
    data: envelope.data ?? envelope.Data,
    message: envelope.message ?? envelope.Message,
  };
};

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
  (response) => {
    response.data = normalizeApiEnvelope(response.data);
    return response;
  },

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

        const refreshPayload = normalizeApiEnvelope(refreshResponse.data) as ApiEnvelope;
        const tokenData = refreshPayload.data as {
          token: { token: string; refreshToken: string };
        };

        const newToken = tokenData.token.token;

        const newRefreshToken = tokenData.token.refreshToken;

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
