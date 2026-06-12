import {
  Dialog,
  DialogTitle,
  DialogContent,
  Typography,
} from "@mui/material";

import {
  useEffect,
  useState,
} from "react";

import { getDesignationByIdApi } from "../../api/designationApi";

import type { Designation } from "../../types/designation";

type Props = {
  open: boolean;
  designationId: number | null;
  onClose: () => void;
};

export default function DesignationDetailsDialog({
  open,
  designationId,
  onClose,
}: Props) {
  const [designation, setDesignation] =
    useState<Designation | null>(
      null
    );

  useEffect(() => {
    const load =
      async () => {
        if (!designationId)
          return;

        const response =
          await getDesignationByIdApi(
            designationId
          );

        setDesignation(
          response.Data
        );
      };

    void load();
  }, [designationId]);

  return (
    <Dialog
      open={open}
      onClose={onClose}
      maxWidth="sm"
      fullWidth
    >
      <DialogTitle>
        Designation Details
      </DialogTitle>

      <DialogContent>
        <Typography>
          ID:{" "}
          {
            designation?.designationId
          }
        </Typography>

        <Typography>
          Name:{" "}
          {designation?.name}
        </Typography>
      </DialogContent>
    </Dialog>
  );
}