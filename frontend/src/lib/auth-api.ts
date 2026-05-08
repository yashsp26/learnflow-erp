import { API_BASE_URL } from "@/lib/config";
import type { LoginPayload, LoginResponse, Session } from "@/types/auth";

export async function loginUser(payload: LoginPayload) {
  const response = await fetch(`${API_BASE_URL}/api/auth/login`, {
    method: "POST",
    headers: {
      accept: "*/*",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  });

  const data = (await response.json()) as LoginResponse;

  if (!response.ok || !data.Success || !data.Data?.token?.token) {
    throw new Error(data.Message || data.Data?.message || "Login failed.");
  }

  return {
    token: data.Data.token.token,
    refreshToken: data.Data.token.refreshToken,
    userId: data.Data.token.userId,
    email: data.Data.token.email,
  } satisfies Session;
}

export async function logoutUser(token: string) {
  await fetch(`${API_BASE_URL}/api/auth/logout`, {
    method: "POST",
    headers: {
      accept: "*/*",
      Authorization: `Bearer ${token}`,
    },
  });
}
