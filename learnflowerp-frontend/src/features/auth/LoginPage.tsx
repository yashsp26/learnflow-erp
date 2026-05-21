import {
  Box,
  Button,
  Card,
  CardContent,
  TextField,
  Typography,
  Link,
} from "@mui/material";

import {
  useState,
} from "react";

import {
  useNavigate,
} from "react-router-dom";

import {
  loginApi,
} from "../../api/authApi";
import { toast } from "react-hot-toast";

export default function LoginPage() {
  const navigate = useNavigate();

  const [tenantCode, setTenantCode] =
    useState("");

  const [email, setEmail] =
    useState("");

  const [password, setPassword] =
    useState("");

  const [loading, setLoading] =
    useState(false);

  const handleLogin = async () => {
    try {
      setLoading(true);

      const response =
        await loginApi(
          email,
          password,
          tenantCode
        );

      const tokenData =
        response.Data.token;

      localStorage.setItem(
        "token",
        tokenData.token
      );

      localStorage.setItem(
        "refreshToken",
        tokenData.refreshToken
      );

      localStorage.setItem(
        "userEmail",
        tokenData.email
      );

      navigate("/dashboard");
    } catch (error: unknown) {
      toast.error(
        (error as { response?: { data?: { Message?: string } } })?.response?.data?.Message ||
          "Login failed"
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
        height: "100vh",
        backgroundColor: "#f5f3ee",
      }}
    >
      <Card
        elevation={0}
        sx={{
          width: 420,
          padding: 2,
          borderRadius: "20px",
          border: "1px solid #ece7df",
        }}
      >
        <CardContent>
          <Typography
            variant="h4"
            sx={{
              marginBottom: 1,
              fontWeight: 700,
            }}
          >
            LearnFlowERP
          </Typography>

          <Typography
            sx={{
              color: "#6b7280",
              marginBottom: 3,
            }}
          >
            Welcome back
          </Typography>

          <TextField
            fullWidth
            label="Tenant Code"
            margin="normal"
            value={tenantCode}
            onChange={(e) =>
              setTenantCode(
                e.target.value
              )
            }
          />

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

          <TextField
            fullWidth
            label="Password"
            type="password"
            margin="normal"
            value={password}
            onChange={(e) =>
              setPassword(
                e.target.value
              )
            }
          />

          <Box
            sx={{
              display: "flex",
              justifyContent: "flex-end",
              marginTop: 1,
            }}
          >
            <Link
              component="button"
              underline="none"
              onClick={() =>
                navigate(
                  "/forgot-password"
                )
              }
              sx={{
                color: "#e86f00",
                fontWeight: 600,
              }}
            >
              Forgot Password?
            </Link>
          </Box>

          <Button
            fullWidth
            variant="contained"
            onClick={handleLogin}
            disabled={loading}
            sx={{
              marginTop: 3,
              padding: 1.5,
              borderRadius: "10px",
              textTransform: "none",
              fontSize: "16px",
              backgroundColor: "#e86f00",
            }}
          >
            {loading
              ? "Logging in..."
              : "Login"}
          </Button>
        </CardContent>
      </Card>
    </Box>
  );
}