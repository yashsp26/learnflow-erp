import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  Stack,
} from "@mui/material";

import { useState } from "react";
import toast from "react-hot-toast";

import { createCourseApi } from "../../api/courseApi";

type Props = {
  open: boolean;
  onClose: () => void;
  onSuccess: () => void;
};

export default function CreateCourseDialog({
  open,
  onClose,
  onSuccess,
}: Props) {
  const [courseCode, setCourseCode] = useState("");

  const [courseName, setCourseName] = useState("");

  const [credits, setCredits] = useState(0);

  const [loading, setLoading] = useState(false);

  const handleSubmit = async () => {
    try {
      setLoading(true);

      await createCourseApi(courseCode, courseName, credits);

      toast.success("Course created successfully");

      onSuccess();
      onClose();

      setCourseCode("");
      setCourseName("");
      setCredits(0);
    } catch {
      toast.error("Failed to create course");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>Create Course</DialogTitle>

      <DialogContent>
        <Stack
          spacing={2}
          sx={{
            mt: 1,
          }}
        >
          <TextField
            label="Course Code"
            value={courseCode}
            onChange={(e) => setCourseCode(e.target.value)}
            fullWidth
          />

          <TextField
            label="Course Name"
            value={courseName}
            onChange={(e) => setCourseName(e.target.value)}
            fullWidth
          />

          <TextField
            label="Credits"
            type="number"
            value={credits}
            onChange={(e) => setCredits(Number(e.target.value))}
            fullWidth
          />
        </Stack>
      </DialogContent>

      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>

        <Button variant="contained" onClick={handleSubmit} disabled={loading}>
          Create
        </Button>
      </DialogActions>
    </Dialog>
  );
}
