"use client";

import { ReactNode, useEffect } from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/context/auth-context";
import { Sidebar } from "@/components/layout/Sidebar";
import { Topbar } from "@/components/layout/Topbar";

export function AppShell({ children }: { children: ReactNode }) {
  const router = useRouter();
  const { session } = useAuth();

  useEffect(() => {
    if (!session) {
      router.replace("/");
    }
  }, [router, session]);

  if (!session) {
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
