import { useAppSelector } from "../../app/hooks";

export const useHasPermission = (permission: string) =>
  useAppSelector((state) =>
    state.permissions.items.includes(permission)
  );
