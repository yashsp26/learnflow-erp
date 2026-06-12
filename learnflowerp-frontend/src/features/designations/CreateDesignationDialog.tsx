import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
} from "@mui/material";

import { useState } from "react";

import toast from "react-hot-toast";

import { createDesignationApi } from "../../api/designationApi";

type Props = {
  open: boolean;
  onClose: () => void;
  onSuccess: () => void;
};

export default function CreateDesignationDialog({
  open,
  onClose,
  onSuccess,
}: Props) {
  const [name, setName] =
    useState("");

  const handleCreate =
    async () => {
      try {
        await createDesignationApi(
          name
        );

        toast.success(
          "Designation created"
        );

        onSuccess();
        onClose();

        setName("");
      } catch {
        toast.error(
          "Failed to create designation"
        );
      }
    };

  return (
    <Dialog
      open={open}
      onClose={onClose}
      maxWidth="sm"
      fullWidth
    >
      <DialogTitle>
        Create Designation
      </DialogTitle>

      <DialogContent>
        <TextField
          fullWidth
          label="Designation Name"
          margin="normal"
          value={name}
          onChange={(e) =>
            setName(
              e.target.value
            )
          }
        />
      </DialogContent>

      <DialogActions>
        <Button
          onClick={onClose}
        >
          Cancel
        </Button>

        <Button
          variant="contained"
          onClick={handleCreate}
        >
          Save
        </Button>
      </DialogActions>
    </Dialog>
  );
}