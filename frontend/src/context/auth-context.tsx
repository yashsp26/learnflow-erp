"use client";

import {
  createContext,
  ReactNode,
  useContext,
  useMemo,
  useSyncExternalStore,
} from "react";
import {
  clearSession,
  getStoredSessionSnapshot,
  parseSessionSnapshot,
  saveSession,
  subscribeToSession,
} from "@/lib/auth-storage";
import { loginUser, logoutUser } from "@/lib/auth-api";
import type { LoginPayload, Session } from "@/types/auth";

const AUTH_PENDING = "__learnflowerp_auth_pending__";

type AuthContextValue = {
  session: Session | null;
  isAuthReady: boolean;
  currentUserName: string;
  login: (payload: LoginPayload) => Promise<void>;
  logout: () => Promise<void>;
};

const AuthContext = createContext<AuthContextValue | null>(null);

export function AuthProvider({ children }: { children: ReactNode }) {
  const sessionSnapshot = useSyncExternalStore(
    subscribeToSession,
    getStoredSessionSnapshot,
    () => AUTH_PENDING,
  );
  const isAuthReady = sessionSnapshot !== AUTH_PENDING;
  const session = useMemo(
    () => (isAuthReady ? parseSessionSnapshot(sessionSnapshot) : null),
    [isAuthReady, sessionSnapshot],
  );

  const currentUserName = session?.email
    ? session.email.split("@")[0].replace(/[._-]/g, " ")
    : "Admin User";

  const value = useMemo<AuthContextValue>(
    () => ({
      session,
      isAuthReady,
      currentUserName,
      async login(payload) {
        const nextSession = await loginUser(payload);

        saveSession(nextSession);
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
      },
    }),
    [currentUserName, isAuthReady, session],
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
