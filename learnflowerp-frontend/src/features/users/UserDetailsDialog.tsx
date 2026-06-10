import {
  Dialog,
  DialogTitle,
  DialogContent,
  Typography,
  Box,
} from "@mui/material";

import {
  useEffect,
  useState,
} from "react";

import toast from "react-hot-toast";

import { getUserByIdApi } from "../../api/userApi";

import type {
  User,
} from "../../types/user";

type Props = {
  open: boolean;
  userId: number | null;
  onClose: () => void;
};

export default function UserDetailsDialog({
  open,
  userId,
  onClose,
}: Props) {
  const [user, setUser] =
    useState<User | null>(null);

  useEffect(() => {
    const loadUser = async () => {
      if (!userId) return;

      try {
        const response =
          await getUserByIdApi(
            userId
          );

        setUser(
          response.Data ??
            response
        );
      } catch {
        toast.error(
          "Failed to load user"
        );
      }
    };

    if (open) {
      void loadUser();
    }
  }, [open, userId]);

  return (
    <Dialog
      open={open}
      onClose={onClose}
      fullWidth
      maxWidth="sm"
    >
      <DialogTitle>
        User Details
      </DialogTitle>

      <DialogContent>
        {user && (
          <Box
            sx={{
              display: "flex",
              flexDirection:
                "column",
              gap: 2,
              marginTop: 2,
            }}
          >
            <Typography>
              <strong>
                Username:
              </strong>{" "}
              {user.username}
            </Typography>

            <Typography>
              <strong>
                Email:
              </strong>{" "}
              {user.email}
            </Typography>
          </Box>
        )}
      </DialogContent>
    </Dialog>
  );
}