import type { Session } from "@/types/auth";

const SESSION_KEY = "learnflowerp-session";
const sessionListeners = new Set<() => void>();

function emitSessionChange() {
  sessionListeners.forEach((listener) => listener());
}

export function getStoredSessionSnapshot() {
  if (typeof window === "undefined") {
    return null;
  }

  const savedSession = window.localStorage.getItem(SESSION_KEY);

  if (!savedSession) {
    return null;
  }

  try {
    JSON.parse(savedSession) as Session;
    return savedSession;
  } catch {
    window.localStorage.removeItem(SESSION_KEY);
    return null;
  }
}

export function parseSessionSnapshot(savedSession: string | null) {
  if (!savedSession) {
    return null;
  }

  try {
    return JSON.parse(savedSession) as Session;
  } catch {
    return null;
  }
}

export function getStoredSession() {
  return parseSessionSnapshot(getStoredSessionSnapshot());
}

export function saveSession(session: Session) {
  window.localStorage.setItem(SESSION_KEY, JSON.stringify(session));
  emitSessionChange();
}

export function clearSession() {
  window.localStorage.removeItem(SESSION_KEY);
  emitSessionChange();
}

export function subscribeToSession(listener: () => void) {
  sessionListeners.add(listener);

  if (typeof window === "undefined") {
    return () => sessionListeners.delete(listener);
  }

  const handleStorage = (event: StorageEvent) => {
    if (event.key === SESSION_KEY) {
      listener();
    }
  };

  window.addEventListener("storage", handleStorage);

  return () => {
    sessionListeners.delete(listener);
    window.removeEventListener("storage", handleStorage);
  };
}
