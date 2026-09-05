import { Button, CircularProgress, Stack, Typography } from "@mui/material";

import { useRef, useState } from "react";

import toast from "react-hot-toast";

import { uploadFileApi } from "../../api/fileApi";

type Props = {
  value?: string | null;
  onUploaded: (url: string) => void;
  accept?: string;
};

export default function FileUpload({ value, onUploaded, accept }: Props) {
  const inputRef = useRef<HTMLInputElement>(null);
  const [uploading, setUploading] = useState(false);

  const handleFile = async (file: File | undefined) => {
    if (!file) return;

    if (file.size > 5 * 1024 * 1024) {
      toast.error("File must be 5MB or smaller");
      return;
    }

    try {
      setUploading(true);
      const response = await uploadFileApi(file);
      const url = response.data ?? response;

      if (typeof url !== "string") {
        throw new Error("The upload response did not include a file URL");
      }

      onUploaded(url);
      toast.success("File uploaded successfully");
    } catch {
      toast.error("Failed to upload file");
    } finally {
      setUploading(false);
    }
  };

  return (
    <Stack spacing={1}>
      {value && (
        <Typography variant="body2" component="a" href={value} target="_blank" rel="noreferrer">
          View current file
        </Typography>
      )}
      <Button
        variant="outlined"
        disabled={uploading}
        onClick={() => inputRef.current?.click()}
        startIcon={uploading ? <CircularProgress size={16} /> : undefined}
      >
        {uploading ? "Uploading..." : "Upload file"}
      </Button>
      <input
        ref={inputRef}
        hidden
        type="file"
        accept={accept}
        onChange={(event) => {
          void handleFile(event.target.files?.[0]);
          event.target.value = "";
        }}
      />
      <Typography variant="caption" color="text.secondary">
        Maximum file size: 5MB
      </Typography>
    </Stack>
  );
}
