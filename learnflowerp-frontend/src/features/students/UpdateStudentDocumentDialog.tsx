import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Stack,
} from "@mui/material";

import { useState } from "react";

import toast from "react-hot-toast";

import { updateStudentDocumentApi } from "../../api/studentApi";
import FileUpload from "../../components/common/FileUpload";

type Props = {
  open: boolean;
  studentId: number | null;
  documentUrl: string | null;
  onClose: () => void;
  onSuccess: () => void;
};

export default function UpdateStudentDocumentDialog({
  open,
  studentId,
  documentUrl,
  onClose,
  onSuccess,
}: Props) {
  const [uploadedUrl, setUploadedUrl] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  const handleUpdate = async () => {
    if (!studentId || !uploadedUrl) {
      toast.error("Please upload a document");
      return;
    }

    try {
      setLoading(true);
      await updateStudentDocumentApi(studentId, uploadedUrl);

      toast.success("Document updated successfully");
      onSuccess();
      onClose();
    } catch {
      toast.error("Failed to update document");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>Update Student Document</DialogTitle>

      <DialogContent>
        <Stack spacing={2} sx={{ mt: 1 }}>
          <FileUpload
            value={uploadedUrl ?? documentUrl}
            onUploaded={setUploadedUrl}
          />
        </Stack>
      </DialogContent>

      <DialogActions>
        <Button onClick={onClose} disabled={loading}>
          Cancel
        </Button>
        <Button variant="contained" onClick={handleUpdate} disabled={loading}>
          Upload and Save
        </Button>
      </DialogActions>
    </Dialog>
  );
}
