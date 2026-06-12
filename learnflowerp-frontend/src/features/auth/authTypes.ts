export interface AuthState {
  token: string | null;
  refreshToken: string | null;
  userId: number | null;
  email: string | null;
  isAuthenticated: boolean;
}

export interface AuthResponse {
  token: string;
  refreshToken: string;
  userId: number;
  email: string;
}