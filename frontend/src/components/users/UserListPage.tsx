"use client";

import { useCallback, useEffect, useState } from "react";
import { getUsers } from "@/lib/users-api";
import { useAuth } from "@/context/auth-context";
import type { User } from "@/types/user";

export function UserListPage() {
  const { session } = useAuth();
  const [users, setUsers] = useState<User[]>([]);
  const [userListError, setUserListError] = useState("");
  const [isLoadingUsers, setIsLoadingUsers] = useState(false);

  const loadUsers = useCallback(async (token?: string) => {
    if (!token) {
      return;
    }

    setIsLoadingUsers(true);
    setUserListError("");

    try {
      const data = await getUsers(token);

      if (!Array.isArray(data.Data)) {
        throw new Error("Unable to load users.");
      }

      setUsers(data.Data);
    } catch (error) {
      setUserListError(
        error instanceof Error ? error.message : "Unable to load users.",
      );
    } finally {
      setIsLoadingUsers(false);
    }
  }, []);

  useEffect(() => {
    const timeoutId = window.setTimeout(() => {
      void loadUsers(session?.token);
    }, 0);

    return () => window.clearTimeout(timeoutId);
  }, [loadUsers, session?.token]);

  return (
    <section className="page-grid">
      <div className="section-heading with-action">
        <div>
          <p className="eyebrow">Directory</p>
          <h1>User List</h1>
        </div>
        <button type="button" onClick={() => void loadUsers(session?.token)}>
          Refresh
        </button>
      </div>

      <div className="erp-card table-card">
        {isLoadingUsers ? <p className="muted">Loading users...</p> : null}
        {userListError ? <p className="form-error">{userListError}</p> : null}

        <table>
          <thead>
            <tr>
              <th>User ID</th>
              <th>Name</th>
              <th>Email</th>
            </tr>
          </thead>
          <tbody>
            {users.map((user) => (
              <tr key={user.userId}>
                <td>{user.userId}</td>
                <td>{user.username}</td>
                <td>{user.email}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </section>
  );
}
