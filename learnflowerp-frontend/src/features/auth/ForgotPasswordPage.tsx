import {
  Box,
  Button,
  Card,
  CardContent,
  TextField,
  Typography,
} from "@mui/material";

import { useState } from "react";

import {
  useNavigate,
} from "react-router-dom";

import {
  forgotPasswordApi,
} from "../../api/authApi";
import { toast } from "react-hot-toast";

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

      navigate(
        `/reset-password?email=${email}`
      );
    } catch {
      toast.error("Failed to fetch users");
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
            Forgot Password
          </Typography>

          <Typography
            sx={{
              color: "#6b7280",
              marginBottom: 4,
            }}
          >
            Enter your email to receive OTP.
          </Typography>

          <TextField
            fullWidth
            label="Email"
            margin="normal"
            value={email}
            onChange={(e) =>
              setEmail(e.target.value)
            }
          />

          <Button
            fullWidth
            variant="contained"
            onClick={handleSendOtp}
            disabled={loading}
            sx={{
              marginTop: 3,
              padding: 1.5,
              backgroundColor: "#e86f00",
              borderRadius: "10px",

              "&:hover": {
                backgroundColor: "#d65f00",
              },
            }}
          >
            {loading
              ? "Sending OTP..."
              : "Send OTP"}
          </Button>
        </CardContent>
      </Card>
    </Box>
  );
}