import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
} from "@mui/material";

import {
  useEffect,
  useState,
} from "react";

import toast from "react-hot-toast";

import {
  getDesignationByIdApi,
  updateDesignationApi,
} from "../../api/designationApi";

type Props = {
  open: boolean;
  designationId: number | null;
  onClose: () => void;
  onSuccess: () => void;
};

export default function EditDesignationDialog({
  open,
  designationId,
  onClose,
  onSuccess,
}: Props) {
  const [name, setName] =
    useState("");

  useEffect(() => {
    const load =
      async () => {
        if (!designationId)
          return;

        const response =
          await getDesignationByIdApi(
            designationId
          );

        setName(
          response.data.name
        );
      };

    void load();
  }, [designationId]);

  const handleUpdate =
    async () => {
      if (!designationId)
        return;

      try {
        await updateDesignationApi(
          designationId,
          name
        );

        toast.success(
          "Designation updated"
        );

        onSuccess();
        onClose();
      } catch {
        toast.error(
          "Failed to update designation"
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
        Edit Designation
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
          onClick={handleUpdate}
        >
          Update
        </Button>
      </DialogActions>
    </Dialog>
  );
}