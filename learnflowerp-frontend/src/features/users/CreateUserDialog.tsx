import {
  Dialog,
  DialogTitle,
  DialogContent,
  TextField,
  Button,
  Box,
} from "@mui/material";

import { useState } from "react";

import {
  createUserApi,
} from "../../api/userApi";
import toast from "react-hot-toast";

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

  const [password, setPassword] =
    useState("");

  const [loading, setLoading] =
    useState(false);

  const handleCreate = async () => {
    try {
      setLoading(true);

      await createUserApi(
        username,
        1,
        email,
        password
      );

      onSuccess();

      onClose();
    } catch {
      toast.error("Failed to create user");
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
            flexDirection: "column",
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
            label="Password"
            type="password"
            value={password}
            onChange={(e) =>
              setPassword(
                e.target.value
              )
            }
            fullWidth
          />

          <Button
            variant="contained"
            onClick={handleCreate}
            disabled={loading}
            sx={{
              backgroundColor:
                "#e86f00",
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