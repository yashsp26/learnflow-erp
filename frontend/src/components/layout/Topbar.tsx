"use client";

import { useRouter } from "next/navigation";
import { useAuth } from "@/context/auth-context";

export function Topbar() {
  const router = useRouter();
  const { currentUserName, logout, session } = useAuth();

  async function handleLogout() {
    await logout();
    router.push("/");
  }

  return (
    <header className="topbar">
      <div className="search-box">
        <span aria-hidden="true">Search</span>
        <input placeholder="Search for anything..." />
      </div>

      <div className="profile">
        <div className="profile-avatar">
          {currentUserName.slice(0, 1).toUpperCase()}
        </div>
        <div>
          <strong>{currentUserName}</strong>
          <span>{session?.email}</span>
        </div>
        <button type="button" onClick={handleLogout}>
          Logout
        </button>
      </div>
    </header>
  );
}
