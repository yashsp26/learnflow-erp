import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

import { getMyPermissionsApi } from "../../api/permissionApi";

type PermissionsState = {
  items: string[];
  loading: boolean;
};

const initialState: PermissionsState = {
  items: [],
  loading: false,
};

export const fetchMyPermissions = createAsyncThunk(
  "permissions/fetchMyPermissions",
  async () => {
    const response = await getMyPermissionsApi();

    return response.data ?? [];
  }
);

const permissionsSlice = createSlice({
  name: "permissions",
  initialState,
  reducers: {
    clearPermissions: (state) => {
      state.items = [];
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchMyPermissions.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchMyPermissions.fulfilled, (state, action) => {
        state.items = action.payload;
        state.loading = false;
      })
      .addCase(fetchMyPermissions.rejected, (state) => {
        state.loading = false;
      });
  },
});

export const { clearPermissions } = permissionsSlice.actions;

export default permissionsSlice.reducer;
