import api from "./axios";

export const getPermissionsApi = async () => {
  const response = await api.get(
    "/permissions"
  );

  return response.data;
};

export const getRolesApi = async () => {
  const response = await api.get(
    "/permissions/roles"
  );

  return response.data;
};

export const getRolePermissionsApi =
  async (roleId: number) => {
    const response =
      await api.get(
        `/permissions/roles/${roleId}`
      );

    return response.data;
  };

export const grantRolePermissionApi =
  async (
    roleId: number,
    permissionId: number
  ) => {
    await api.post(
      `/permissions/roles/${roleId}/${permissionId}`
    );
  };

export const revokeRolePermissionApi =
  async (
    roleId: number,
    permissionId: number
  ) => {
    await api.delete(
      `/permissions/roles/${roleId}/${permissionId}`
    );
  };

export const getDesignationsApi = async () => {
  const response = await api.get(
    "/designations"
  );

  return response.data;
};

export const getDesignationPermissionsApi =
  async (
    designationId: number
  ) => {
    const response =
      await api.get(
        `/permissions/designations/${designationId}`
      );

    return response.data;
  };

export const grantDesignationPermissionApi =
  async (
    designationId: number,
    permissionId: number
  ) => {
    await api.post(
      `/permissions/designations/${designationId}/${permissionId}`
    );
  };

export const revokeDesignationPermissionApi =
  async (
    designationId: number,
    permissionId: number
  ) => {
    await api.delete(
      `/permissions/designations/${designationId}/${permissionId}`
    );
  };