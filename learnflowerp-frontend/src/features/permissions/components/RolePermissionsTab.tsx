import {
  Box,
  Checkbox,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  Typography,
} from "@mui/material";

import { useEffect, useState } from "react";

import toast from "react-hot-toast";

import {
  getPermissionsApi,
  getRolesApi,
  getRolePermissionsApi,
  grantRolePermissionApi,
  revokeRolePermissionApi,
} from "../../../api/permissionApi";

type Permission = {
  permissionId: number;
  name: string;
};

type Role = {
  roleId: number;
  roleName: string;
};

export default function RolePermissionsTab() {
  const [roles, setRoles] = useState<Role[]>([]);

  const [permissions, setPermissions] = useState<Permission[]>([]);

  const [selectedRole, setSelectedRole] = useState<number>();

  const [assignedPermissions, setAssignedPermissions] = useState<string[]>([]);

  useEffect(() => {
    const loadData = async () => {
      try {
        const rolesResponse = await getRolesApi();

        const permissionsResponse = await getPermissionsApi();

        setRoles(rolesResponse.data ?? []);

        setPermissions(permissionsResponse.data ?? []);
      } catch {
        toast.error("Failed to load roles");
      }
    };

    void loadData();
  }, []);

  const loadRolePermissions = async (roleId: number) => {
    const response = await getRolePermissionsApi(roleId);

    setAssignedPermissions(response.data ?? []);
  };

  return (
    <Box>
      <FormControl
        fullWidth
        sx={{
          marginBottom: 3,
        }}
      >
        <InputLabel>Role</InputLabel>

        <Select
          value={selectedRole ?? ""}
          label="Role"
          onChange={async (e) => {
            const roleId = Number(e.target.value);

            setSelectedRole(roleId);

            await loadRolePermissions(roleId);
          }}
        >
          {roles.map((role) => (
            <MenuItem key={role.roleId} value={role.roleId}>
              {role.roleName}
            </MenuItem>
          ))}
        </Select>
      </FormControl>

      {permissions.map((permission) => (
        <Box
          key={permission.permissionId}
          sx={{
            display: "flex",
            alignItems: "center",
          }}
        >
          <Checkbox
            checked={assignedPermissions.includes(permission.name)}
            onChange={async (e) => {
              if (!selectedRole) return;

              try {
                if (e.target.checked) {
                  await grantRolePermissionApi(
                    selectedRole,
                    permission.permissionId,
                  );
                } else {
                  await revokeRolePermissionApi(
                    selectedRole,
                    permission.permissionId,
                  );
                }

                await loadRolePermissions(selectedRole);

                toast.success("Permission updated");
              } catch {
                toast.error("Failed to update permission");
              }
            }}
          />

          <Typography>{permission.name}</Typography>
        </Box>
      ))}
    </Box>
  );
}
