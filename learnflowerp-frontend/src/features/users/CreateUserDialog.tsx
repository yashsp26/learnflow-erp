import {
  Dialog,
  DialogTitle,
  DialogContent,
  TextField,
  Button,
  Box,
  MenuItem,
  Typography,
} from "@mui/material";

import { useState } from "react";

import toast from "react-hot-toast";

import { createUserApi } from "../../api/userApi";

type Props = {
  open: boolean;
  onClose: () => void;
  onSuccess: () => void;
};

export default function CreateUserDialog({
  open,
  onClose,
  onSuccess,
}: Props) {
  const [username, setUsername] =
    useState("");

  const [email, setEmail] =
    useState("");

  const [roleId, setRoleId] =
    useState(1);

  const [loading, setLoading] =
    useState(false);

  const handleCreate = async () => {
    if (!username.trim()) {
      toast.error(
        "Username is required"
      );

      return;
    }

    if (!email.trim()) {
      toast.error(
        "Email is required"
      );

      return;
    }

    try {
      setLoading(true);

      await createUserApi(
        username,
        roleId,
        email
      );

      toast.success(
        "User created successfully"
      );

      setUsername("");
      setEmail("");
      setRoleId(1);

      onSuccess();

      onClose();
    } catch {
      toast.error(
        "Failed to create user"
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <Dialog
      open={open}
      onClose={onClose}
      fullWidth
      maxWidth="sm"
    >
      <DialogTitle>
        Create User
      </DialogTitle>

      <DialogContent>
        <Box
          sx={{
            display: "flex",
            flexDirection:
              "column",
            gap: 2,
            marginTop: 2,
          }}
        >
          <TextField
            label="Username"
            value={username}
            onChange={(e) =>
              setUsername(
                e.target.value
              )
            }
            fullWidth
          />

          <TextField
            label="Email"
            value={email}
            onChange={(e) =>
              setEmail(
                e.target.value
              )
            }
            fullWidth
          />

          <TextField
            select
            label="Role"
            value={roleId}
            onChange={(e) =>
              setRoleId(
                Number(
                  e.target.value
                )
              )
            }
            fullWidth
          >
            <MenuItem value={1}>
              Admin
            </MenuItem>

            <MenuItem value={2}>
              Student
            </MenuItem>

            <MenuItem value={3}>
              Employee
            </MenuItem>
          </TextField>

          <Typography
            sx={{
              fontSize: "14px",
              color: "#6b7280",
              backgroundColor:
                "#fff7ed",
              padding: 2,
              borderRadius: "12px",
              border:
                "1px solid #fed7aa",
            }}
          >
            A temporary password will
            be generated automatically
            and sent to the user's
            email address.
          </Typography>

          <Button
            variant="contained"
            onClick={handleCreate}
            disabled={loading}
            sx={{
              borderRadius: "12px",
              textTransform: "none",
              height: 48,
            }}
          >
            {loading
              ? "Creating..."
              : "Create User"}
          </Button>
        </Box>
      </DialogContent>
    </Dialog>
  );
}