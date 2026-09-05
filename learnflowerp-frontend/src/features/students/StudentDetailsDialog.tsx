import {
  Dialog,
  DialogContent,
  DialogTitle,
  Divider,
  Link,
  Stack,
  Typography,
} from "@mui/material";

import { useEffect, useState } from "react";

import toast from "react-hot-toast";

import { getStudentByIdApi } from "../../api/studentApi";
import { updateStudentDocumentApi } from "../../api/studentApi";

import FileUpload from "../../components/common/FileUpload";
import RequirePermission from "../../components/common/RequirePermission";

import type { Student } from "../../types/student";

type Props = {
  open: boolean;
  studentId: number | null;
  onClose: () => void;
};

const displayValue = (value: string | number | null) => value ?? "—";

const formatDate = (value: string | null) => {
  if (!value) return "—";

  return new Date(value).toLocaleDateString();
};

export default function StudentDetailsDialog({
  open,
  studentId,
  onClose,
}: Props) {
  const [student, setStudent] = useState<Student | null>(null);
  const [uploadedUrl, setUploadedUrl] = useState<string | null>(null);

  useEffect(() => {
    const loadStudent = async () => {
      if (!studentId) return;

      try {
        const response = await getStudentByIdApi(studentId);
        const payload = response.data ?? response;

        setStudent(payload as Student);
      } catch {
        toast.error("Failed to load student");
      }
    };

    if (open) void loadStudent();
  }, [open, studentId]);

  useEffect(() => {
    const updateDocument = async () => {
      if (!studentId || !uploadedUrl) return;

      try {
        await updateStudentDocumentApi(studentId, uploadedUrl);
        setStudent((current) => current ? { ...current, documentUrl: uploadedUrl } : current);
        toast.success("Document updated successfully");
      } catch {
        toast.error("Failed to update document");
      }
    };

    void updateDocument();
  }, [studentId, uploadedUrl]);

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>Student Details</DialogTitle>

      <DialogContent>
        {student && (
          <Stack spacing={2}>
            <Divider />
            <Typography>
              <strong>Student ID:</strong> {student.studentId}
            </Typography>
            <RequirePermission name="UpdateStudent">
              <FileUpload
                value={student.documentUrl}
                onUploaded={setUploadedUrl}
              />
            </RequirePermission>
            <Typography>
              <strong>User ID:</strong> {displayValue(student.userId)}
            </Typography>
            <Typography>
              <strong>Enrollment No:</strong> {student.enrollmentNo}
            </Typography>
            <Typography>
              <strong>First Name:</strong> {displayValue(student.firstName)}
            </Typography>
            <Typography>
              <strong>Last Name:</strong> {displayValue(student.lastName)}
            </Typography>
            <Typography>
              <strong>Email:</strong> {student.email}
            </Typography>
            <Typography>
              <strong>Date of Birth:</strong> {formatDate(student.dob)}
            </Typography>
            <Typography>
              <strong>Document:</strong>{" "}
              {student.documentUrl ? (
                <Link
                  href={student.documentUrl}
                  target="_blank"
                  rel="noreferrer"
                >
                  View document
                </Link>
              ) : (
                "—"
              )}
            </Typography>
          </Stack>
        )}
      </DialogContent>
    </Dialog>
  );
}
