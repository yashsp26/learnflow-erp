import { AppShell } from "@/components/layout/AppShell";
import { UserListPage } from "@/components/users/UserListPage";

export default function UserListRoute() {
  return (
    <AppShell>
      <UserListPage />
    </AppShell>
  );
}
