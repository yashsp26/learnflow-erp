import { createSlice } from "@reduxjs/toolkit";

import type { PayloadAction } from "@reduxjs/toolkit";
import type {
  AuthResponse,
  AuthState,
} from "./authTypes";

const initialState: AuthState = {
  token: localStorage.getItem("token"),
  refreshToken:
    localStorage.getItem("refreshToken"),
  userId: Number(
    localStorage.getItem("userId")
  ) || null,
  email: localStorage.getItem("email"),
  isAuthenticated:
    !!localStorage.getItem("token"),
};

const authSlice = createSlice({
  name: "auth",

  initialState,

  reducers: {
    loginSuccess: (
      state,
      action: PayloadAction<AuthResponse>
    ) => {
      const {
        token,
        refreshToken,
        userId,
        email,
      } = action.payload;

      state.token = token;
      state.refreshToken = refreshToken;
      state.userId = userId;
      state.email = email;
      state.isAuthenticated = true;

      localStorage.setItem(
        "token",
        token
      );

      localStorage.setItem(
        "refreshToken",
        refreshToken
      );

      localStorage.setItem(
        "userId",
        userId.toString()
      );

      localStorage.setItem(
        "email",
        email
      );
    },

    updateTokens: (
      state,
      action: PayloadAction<{
        token: string;
        refreshToken: string;
      }>
    ) => {
      state.token =
        action.payload.token;

      state.refreshToken =
        action.payload.refreshToken;

      localStorage.setItem(
        "token",
        action.payload.token
      );

      localStorage.setItem(
        "refreshToken",
        action.payload.refreshToken
      );
    },

    logout: (state) => {
      state.token = null;
      state.refreshToken = null;
      state.userId = null;
      state.email = null;
      state.isAuthenticated = false;

      localStorage.clear();
    },
  },
});

export const {
  loginSuccess,
  updateTokens,
  logout,
} = authSlice.actions;

export default authSlice.reducer;