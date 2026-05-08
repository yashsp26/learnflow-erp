import { API_BASE_URL } from "@/lib/config";
import type { ApiResponse } from "@/types/api";

export async function apiRequest<T>(
  path: string,
  token?: string,
  options: RequestInit = {},
) {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    ...options,
    headers: {
      accept: "*/*",
      ...(options.body instanceof FormData
        ? {}
        : { "Content-Type": "application/json" }),
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...options.headers,
    },
  });

  const data = (await response.json()) as ApiResponse<T>;

  if (!response.ok || data.Success === false) {
    throw new Error(data.Message || "Request failed. Please try again.");
  }

  return data;
}
