import type { Session } from "@/types/auth";

const SESSION_KEY = "learnflowerp-session";

export function getStoredSession() {
  if (typeof window === "undefined") {
    return null;
  }

  const savedSession = window.localStorage.getItem(SESSION_KEY);

  if (!savedSession) {
    return null;
  }

  try {
    return JSON.parse(savedSession) as Session;
  } catch {
    window.localStorage.removeItem(SESSION_KEY);
    return null;
  }
}

export function saveSession(session: Session) {
  window.localStorage.setItem(SESSION_KEY, JSON.stringify(session));
}

export function clearSession() {
  window.localStorage.removeItem(SESSION_KEY);
}
