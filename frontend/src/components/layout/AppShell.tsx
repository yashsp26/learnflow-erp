"use client";

import { ReactNode, useEffect } from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/context/auth-context";
import { Sidebar } from "@/components/layout/Sidebar";
import { Topbar } from "@/components/layout/Topbar";

export function AppShell({ children }: { children: ReactNode }) {
  const router = useRouter();
  const { isAuthReady, session } = useAuth();

  useEffect(() => {
    if (isAuthReady && !session) {
      router.replace("/");
    }
  }, [isAuthReady, router, session]);

  if (!isAuthReady || !session) {
    return null;
  }

  return (
    <main className="erp-shell">
      <Sidebar />

      <section className="workspace">
        <Topbar />
        <section className="content-area">{children}</section>
      </section>
    </main>
  );
}
