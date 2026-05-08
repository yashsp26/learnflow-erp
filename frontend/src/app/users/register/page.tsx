import { AppShell } from "@/components/layout/AppShell";
import { UserRegistrationPage } from "@/components/users/UserRegistrationPage";

export default function UserRegistrationRoute() {
  return (
    <AppShell>
      <UserRegistrationPage />
    </AppShell>
  );
}
