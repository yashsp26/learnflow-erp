import {
  Dialog,
  DialogTitle,
  DialogContent,
  Typography,
  Stack,
  Divider,
} from "@mui/material";

import {
  useEffect,
  useState,
} from "react";

import toast from "react-hot-toast";

import {
  getCourseByIdApi,
} from "../../api/courseApi";

import type {
  Course,
} from "../../types/course";

type Props = {
  open: boolean;
  courseId: number | null;
  onClose: () => void;
};

export default function CourseDetailsDialog({
  open,
  courseId,
  onClose,
}: Props) {
  const [course, setCourse] =
    useState<Course | null>(
      null
    );

  useEffect(() => {
    const loadCourse =
      async () => {
        if (!courseId) return;

        try {
          const response =
            await getCourseByIdApi(
              courseId
            );

          setCourse(
            response.data ??
              response
          );
        } catch {
          toast.error(
            "Failed to load course"
          );
        }
      };

    if (open) {
      loadCourse();
    }
  }, [courseId, open]);

  return (
    <Dialog
      open={open}
      onClose={onClose}
      maxWidth="sm"
      fullWidth
    >
      <DialogTitle>
        Course Details
      </DialogTitle>

      <DialogContent>
        {course && (
          <Stack spacing={2}>
            <Divider />

            <Typography>
              <strong>
                Course Code:
              </strong>{" "}
              {course.courseCode}
            </Typography>

            <Typography>
              <strong>
                Course Name:
              </strong>{" "}
              {course.courseName}
            </Typography>

            <Typography>
              <strong>
                Credits:
              </strong>{" "}
              {course.credits}
            </Typography>
          </Stack>
        )}
      </DialogContent>
    </Dialog>
  );
}