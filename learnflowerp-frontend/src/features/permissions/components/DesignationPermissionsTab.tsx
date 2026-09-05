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
  getDesignationPermissionsApi,
  grantDesignationPermissionApi,
  revokeDesignationPermissionApi,
} from "../../../api/permissionApi";

import { getDesignationsApi } from "../../../api/designationApi";

type Permission = {
  permissionId: number;
  name: string;
};

type Designation = {
  designationId: number;
  name: string;
};

export default function DesignationPermissionsTab() {
  const [designations, setDesignations] = useState<Designation[]>([]);

  const [permissions, setPermissions] = useState<Permission[]>([]);

  const [selectedDesignation, setSelectedDesignation] = useState<number>();

  const [assignedPermissions, setAssignedPermissions] = useState<string[]>([]);

  useEffect(() => {
    const loadData = async () => {
      try {
        const [designationsResponse, permissionsResponse] = await Promise.all([
          getDesignationsApi(),
          getPermissionsApi(),
        ]);

        setDesignations(designationsResponse.data ?? []);

        setPermissions(permissionsResponse.data ?? []);
      } catch {
        toast.error("Failed to load data");
      }
    };

    void loadData();
  }, []);

  const loadPermissions = async (designationId: number) => {
    const response = await getDesignationPermissionsApi(designationId);

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
        <InputLabel>Designation</InputLabel>

        <Select
          value={selectedDesignation ?? ""}
          label="Designation"
          onChange={async (e) => {
            const id = Number(e.target.value);

            setSelectedDesignation(id);

            await loadPermissions(id);
          }}
        >
          {designations.map((designation) => (
            <MenuItem
              key={designation.designationId}
              value={designation.designationId}
            >
              {designation.name}
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
              if (!selectedDesignation) return;

              try {
                if (e.target.checked) {
                  await grantDesignationPermissionApi(
                    selectedDesignation,
                    permission.permissionId,
                  );
                } else {
                  await revokeDesignationPermissionApi(
                    selectedDesignation,
                    permission.permissionId,
                  );
                }

                await loadPermissions(selectedDesignation);

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
