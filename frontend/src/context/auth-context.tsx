"use client";

import {
  createContext,
  ReactNode,
  useContext,
  useMemo,
  useState,
} from "react";
import { clearSession, getStoredSession, saveSession } from "@/lib/auth-storage";
import { loginUser, logoutUser } from "@/lib/auth-api";
import type { LoginPayload, Session } from "@/types/auth";

type AuthContextValue = {
  session: Session | null;
  currentUserName: string;
  login: (payload: LoginPayload) => Promise<void>;
  logout: () => Promise<void>;
};

const AuthContext = createContext<AuthContextValue | null>(null);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [session, setSession] = useState<Session | null>(() =>
    getStoredSession(),
  );

  const currentUserName = session?.email
    ? session.email.split("@")[0].replace(/[._-]/g, " ")
    : "Admin User";

  const value = useMemo<AuthContextValue>(
    () => ({
      session,
      currentUserName,
      async login(payload) {
        const nextSession = await loginUser(payload);

        saveSession(nextSession);
        setSession(nextSession);
      },
      async logout() {
        if (session?.token) {
          try {
            await logoutUser(session.token);
          } catch {
            // Local logout should still happen if the API is unavailable.
          }
        }

        clearSession();
        setSession(null);
      },
    }),
    [currentUserName, session],
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("useAuth must be used inside AuthProvider.");
  }

  return context;
}
