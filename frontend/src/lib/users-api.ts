import { apiRequest } from "@/lib/api";
import type { CreateUserPayload, User } from "@/types/user";

export function createUser(token: string, payload: CreateUserPayload) {
  return apiRequest<number>("/api/users", token, {
    method: "POST",
    body: JSON.stringify(payload),
  });
}

export function getUsers(token: string) {
  return apiRequest<User[]>("/api/users", token);
}
