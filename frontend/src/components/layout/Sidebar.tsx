"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import { menuItems } from "@/components/layout/MenuItems";

export function Sidebar() {
  const pathname = usePathname();

  return (
    <aside className="sidebar">
      <div className="sidebar-brand">
        <div className="logo-circle">LF</div>
        <div>
          <strong>LearnFlow</strong>
          <span>ERP System</span>
        </div>
      </div>

      <nav className="menu-list" aria-label="Main menu">
        {menuItems.map((item) => (
          <Link
            key={item.href}
            href={item.href}
            className={pathname === item.href ? "active" : ""}
          >
            <span aria-hidden="true">{item.icon}</span>
            {item.label}
          </Link>
        ))}
      </nav>
    </aside>
  );
}
