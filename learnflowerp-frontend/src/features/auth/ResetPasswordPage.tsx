import {
  Box,
  Button,
  Card,
  CardContent,
  TextField,
  Typography,
} from "@mui/material";

import {
  useNavigate,
  useSearchParams,
} from "react-router-dom";

import { useState } from "react";

import {
  resetPasswordApi,
} from "../../api/authApi";
import { toast } from "react-hot-toast";

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
        await resetPasswordApi(
          email,
          otp,
          newPassword
        );

        toast.success(
          "Password updated"
        );

        navigate("/");
      } catch {
        toast.error(
          "Failed to reset password"
        );
      }
    };

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
        backgroundColor: "#f5f3ee",
      }}
    >
      <Card
        elevation={0}
        sx={{
          width: 420,
          borderRadius: "24px",
          border: "1px solid #ece7df",
        }}
      >
        <CardContent sx={{ padding: 5 }}>
          <Typography
            variant="h4"
            sx={{
              fontWeight: 700,
              marginBottom: 1,
            }}
          >
            Reset Password
          </Typography>

          <Typography
            sx={{
              color: "#6b7280",
              marginBottom: 4,
            }}
          >
            Enter OTP and new password.
          </Typography>

          <TextField
            fullWidth
            label="OTP"
            margin="normal"
            value={otp}
            onChange={(e) =>
              setOtp(e.target.value)
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
            sx={{
              marginTop: 3,
              padding: 1.5,
              backgroundColor: "#e86f00",
              borderRadius: "10px",
            }}
          >
            Reset Password
          </Button>
        </CardContent>
      </Card>
    </Box>
  );
}