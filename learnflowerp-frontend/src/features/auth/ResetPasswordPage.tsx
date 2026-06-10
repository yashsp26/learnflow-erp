import {
  Box,
  Button,
  TextField,
} from "@mui/material";

import {
  useNavigate,
  useSearchParams,
} from "react-router-dom";

import { useState } from "react";

import { resetPasswordApi } from "../../api/authApi";

import toast from "react-hot-toast";

import AuthCard from "../../components/common/AuthCard";

export default function ResetPasswordPage() {
  const navigate = useNavigate();

  const [searchParams] =
    useSearchParams();

  const email =
    searchParams.get("email") || "";

  const [otp, setOtp] =
    useState("");

  const [newPassword, setNewPassword] =
    useState("");

  const [confirmPassword, setConfirmPassword] =
    useState("");

  const [loading, setLoading] =
    useState(false);

  const handleResetPassword =
    async () => {
      if (
        newPassword !==
        confirmPassword
      ) {
        toast.error(
          "Passwords do not match"
        );
        return;
      }

      try {
        setLoading(true);

        await resetPasswordApi(
          email,
          otp,
          newPassword
        );

        toast.success(
          "Password updated successfully"
        );

        navigate("/");
      } catch {
        toast.error(
          "Failed to reset password"
        );
      } finally {
        setLoading(false);
      }
    };

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        minHeight: "100vh",
        backgroundColor: "#f5f3ee",
      }}
    >
      <AuthCard
        title="Reset Password"
        subtitle="Enter OTP and your new password"
      >
        <TextField
          fullWidth
          label="OTP"
          margin="normal"
          value={otp}
          onChange={(e) =>
            setOtp(
              e.target.value
            )
          }
        />

        <TextField
          fullWidth
          label="New Password"
          type="password"
          margin="normal"
          value={newPassword}
          onChange={(e) =>
            setNewPassword(
              e.target.value
            )
          }
        />

        <TextField
          fullWidth
          label="Confirm Password"
          type="password"
          margin="normal"
          value={confirmPassword}
          onChange={(e) =>
            setConfirmPassword(
              e.target.value
            )
          }
        />

        <Button
          fullWidth
          variant="contained"
          onClick={
            handleResetPassword
          }
          disabled={loading}
          sx={{
            mt: 3,
            py: 1.5,
            borderRadius: "10px",
            textTransform: "none",
          }}
        >
          {loading
            ? "Updating..."
            : "Reset Password"}
        </Button>
      </AuthCard>
    </Box>
  );
}