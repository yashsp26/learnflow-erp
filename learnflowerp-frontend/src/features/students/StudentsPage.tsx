import { Box, Button, Chip, IconButton } from "@mui/material";

import { DataGrid } from "@mui/x-data-grid";
import type { GridColDef, GridPaginationModel } from "@mui/x-data-grid";

import {
  DeleteOutlined,
  EditOutlined,
  AddOutlined,
  VisibilityOutlined,
} from "@mui/icons-material";

import { useCallback, useEffect, useState } from "react";

import toast from "react-hot-toast";

import { deleteStudentApi, getStudentsApi } from "../../api/studentApi";

import ConfirmDialog from "../../components/common/ConfirmDialog";
import PageCard from "../../components/common/PageCard";
import PageHeader from "../../components/common/PageHeader";
import RequirePermission from "../../components/common/RequirePermission";
import SearchBar from "../../components/common/SearchBar";

import type { Student } from "../../types/student";

import StudentDetailsDialog from "./StudentDetailsDialog";
import UpdateStudentDocumentDialog from "./UpdateStudentDocumentDialog";
import CreateUserDialog from "../users/CreateUserDialog";

type PaginatedStudents = {
  items: Student[];
  totalCount: number;
};

const formatDate = (value: string | null) => {
  if (!value) return "—";

  return new Date(value).toLocaleDateString();
};

export default function StudentsPage() {
  const [students, setStudents] = useState<Student[]>([]);
  const [loading, setLoading] = useState(false);
  const [search, setSearch] = useState("");
  const [totalCount, setTotalCount] = useState(0);
  const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({
    page: 0,
    pageSize: 10,
  });
  const [selectedStudent, setSelectedStudent] = useState<Student | null>(null);
  const [openDetails, setOpenDetails] = useState(false);
  const [openDocument, setOpenDocument] = useState(false);
  const [deleteStudentId, setDeleteStudentId] = useState<number | null>(null);
  const [confirmDelete, setConfirmDelete] = useState(false);
  const [openCreate, setOpenCreate] = useState(false);

  const fetchStudents = useCallback(async () => {
    try {
      setLoading(true);

      const response = await getStudentsApi(
        paginationModel.page + 1,
        paginationModel.pageSize,
        search
      );
      const payload = response.data ?? response;
      const studentsPage = payload as PaginatedStudents;

      setStudents(studentsPage.items ?? []);
      setTotalCount(studentsPage.totalCount ?? 0);
    } catch {
      toast.error("Failed to load students");
    } finally {
      setLoading(false);
    }
  }, [paginationModel.page, paginationModel.pageSize, search]);

  useEffect(() => {
    void fetchStudents();
  }, [fetchStudents]);

  const handleSearch = (value: string) => {
    setSearch(value);
    setPaginationModel((current) => ({ ...current, page: 0 }));
  };

  const handleDelete = async () => {
    if (!deleteStudentId) return;

    try {
      await deleteStudentApi(deleteStudentId);
      toast.success("Student deleted successfully");
      setConfirmDelete(false);
      await fetchStudents();
    } catch {
      toast.error("Failed to delete student");
    }
  };

  const columns: GridColDef<Student>[] = [
    { field: "enrollmentNo", headerName: "Enrollment No", flex: 1 },
    {
      field: "firstName",
      headerName: "First Name",
      flex: 1,
      valueGetter: (_value, row) => row.firstName ?? "—",
    },
    {
      field: "lastName",
      headerName: "Last Name",
      flex: 1,
      valueGetter: (_value, row) => row.lastName ?? "—",
    },
    { field: "email", headerName: "Email", flex: 1.5 },
    {
      field: "dob",
      headerName: "DOB",
      width: 130,
      valueGetter: (_value, row) => formatDate(row.dob),
    },
    {
      field: "documentUrl",
      headerName: "Document",
      width: 125,
      sortable: false,
      renderCell: (params) => (
        <Chip
          size="small"
          color={params.row.documentUrl ? "success" : "default"}
          label={params.row.documentUrl ? "Uploaded" : "Missing"}
        />
      ),
    },
    {
      field: "actions",
      headerName: "Actions",
      width: 170,
      sortable: false,
      renderCell: (params) => (
        <Box>
          <IconButton
            aria-label="View student"
            onClick={() => {
              setSelectedStudent(params.row);
              setOpenDetails(true);
            }}
          >
            <VisibilityOutlined />
          </IconButton>

          <RequirePermission name="UpdateStudent"><IconButton aria-label="Update student document" onClick={() => { setSelectedStudent(params.row); setOpenDocument(true); }}><EditOutlined /></IconButton></RequirePermission>

          <RequirePermission name="DeleteStudent"><IconButton
            color="error"
            aria-label="Delete student"
            onClick={() => {
              setDeleteStudentId(params.row.studentId);
              setConfirmDelete(true);
            }}
          >
            <DeleteOutlined />
          </IconButton></RequirePermission>
        </Box>
      ),
    },
  ];

  return (
    <Box>
      <PageHeader title="Students" subtitle="Manage all students" action={<RequirePermission name="CreateUser"><Button variant="contained" startIcon={<AddOutlined />} onClick={() => setOpenCreate(true)}>Add Student</Button></RequirePermission>} />

      <Box sx={{ mb: 3 }}>
        <SearchBar
          value={search}
          onChange={handleSearch}
          placeholder="Search students..."
        />
      </Box>

      <PageCard>
        <Box sx={{ height: 600 }}>
          <DataGrid
            rows={students}
            columns={columns}
            loading={loading}
            getRowId={(row) => row.studentId}
            disableRowSelectionOnClick
            paginationMode="server"
            rowCount={totalCount}
            paginationModel={paginationModel}
            onPaginationModelChange={setPaginationModel}
            pageSizeOptions={[10, 20, 50]}
            sx={{
              border: "none",
              "& .MuiDataGrid-columnHeaders": { backgroundColor: "#f9fafb" },
              "& .MuiDataGrid-row:hover": { backgroundColor: "#fffaf5" },
            }}
          />
        </Box>
      </PageCard>

      <StudentDetailsDialog
        open={openDetails}
        studentId={selectedStudent?.studentId ?? null}
        onClose={() => setOpenDetails(false)}
      />

      <UpdateStudentDocumentDialog
        open={openDocument}
        studentId={selectedStudent?.studentId ?? null}
        documentUrl={selectedStudent?.documentUrl ?? null}
        onClose={() => setOpenDocument(false)}
        onSuccess={fetchStudents}
      />

      <ConfirmDialog
        open={confirmDelete}
        title="Delete Student"
        message="Are you sure you want to delete this student?"
        onConfirm={handleDelete}
        onClose={() => setConfirmDelete(false)}
      />

      <CreateUserDialog
        open={openCreate}
        onClose={() => setOpenCreate(false)}
        onSuccess={() => setOpenCreate(false)}
        defaultRoleId={2}
        title="Add Student — create login"
        onboardingMessage="The student record appears after this user completes their profile on first login."
      />
    </Box>
  );
}
