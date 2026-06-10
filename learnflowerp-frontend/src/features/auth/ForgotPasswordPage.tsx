import {
  Box,
  Button,
  TextField,
} from "@mui/material";

import { useState } from "react";

import { useNavigate } from "react-router-dom";

import { forgotPasswordApi } from "../../api/authApi";

import toast from "react-hot-toast";

import AuthCard from "../../components/common/AuthCard";

export default function ForgotPasswordPage() {
  const navigate = useNavigate();

  const [email, setEmail] =
    useState("");

  const [loading, setLoading] =
    useState(false);

  const handleSendOtp = async () => {
    try {
      setLoading(true);

      await forgotPasswordApi(email);

      toast.success(
        "OTP sent successfully"
      );

      navigate(
        `/reset-password?email=${email}`
      );
    } catch {
      toast.error(
        "Failed to send OTP"
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
        title="Forgot Password"
        subtitle="Enter your email to receive OTP"
      >
        <TextField
          fullWidth
          label="Email"
          margin="normal"
          value={email}
          onChange={(e) =>
            setEmail(
              e.target.value
            )
          }
        />

        <Button
          fullWidth
          variant="contained"
          onClick={handleSendOtp}
          disabled={loading}
          sx={{
            mt: 3,
            py: 1.5,
            borderRadius: "10px",
            textTransform: "none",
          }}
        >
          {loading
            ? "Sending OTP..."
            : "Send OTP"}
        </Button>
      </AuthCard>
    </Box>
  );
}