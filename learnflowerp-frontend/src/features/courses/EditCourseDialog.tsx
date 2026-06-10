import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  Stack,
} from "@mui/material";

import { useEffect, useState } from "react";

import toast from "react-hot-toast";

import { getCourseByIdApi, updateCourseApi } from "../../api/courseApi";

type Props = {
  open: boolean;
  courseId: number | null;
  onClose: () => void;
  onSuccess: () => void;
};

export default function EditCourseDialog({
  open,
  courseId,
  onClose,
  onSuccess,
}: Props) {
  const [courseCode, setCourseCode] = useState("");

  const [courseName, setCourseName] = useState("");

  const [credits, setCredits] = useState(0);

  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const loadCourse = async () => {
      if (!courseId) return;

      try {
        const response = await getCourseByIdApi(courseId);

        const course = response.data ?? response;

        setCourseCode(course.courseCode);

        setCourseName(course.courseName);

        setCredits(course.credits);
      } catch {
        toast.error("Failed to load course");
      }
    };

    if (open) {
      loadCourse();
    }
  }, [courseId, open]);

  const handleUpdate = async () => {
    if (!courseId) return;

    try {
      setLoading(true);

      await updateCourseApi(courseId, courseCode, courseName, credits);

      toast.success("Course updated");

      onSuccess();
      onClose();
    } catch {
      toast.error("Failed to update course");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>Edit Course</DialogTitle>

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
          />

          <TextField
            label="Course Name"
            value={courseName}
            onChange={(e) => setCourseName(e.target.value)}
          />

          <TextField
            label="Credits"
            type="number"
            value={credits}
            onChange={(e) => setCredits(Number(e.target.value))}
          />
        </Stack>
      </DialogContent>

      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>

        <Button variant="contained" onClick={handleUpdate} disabled={loading}>
          Save
        </Button>
      </DialogActions>
    </Dialog>
  );
}
