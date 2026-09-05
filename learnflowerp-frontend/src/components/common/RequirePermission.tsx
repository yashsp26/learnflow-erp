import type { ReactNode } from "react";

import { useHasPermission } from "../../features/permissions/useHasPermission";

type Props = {
  name: string;
  children: ReactNode;
};

export default function RequirePermission({
  name,
  children,
}: Props) {
  const hasPermission = useHasPermission(name);

  if (!hasPermission) return null;

  return <>{children}</>;
}
