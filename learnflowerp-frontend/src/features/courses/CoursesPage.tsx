import {
  Box,
  Button,
  IconButton,
} from "@mui/material";

import { DataGrid } from "@mui/x-data-grid";
import type { GridColDef } from "@mui/x-data-grid";

import {
  VisibilityOutlined,
  EditOutlined,
  DeleteOutlined,
  AddOutlined,
} from "@mui/icons-material";

import { useCallback, useEffect, useState } from "react";

import toast from "react-hot-toast";

import SearchBar from "../../components/common/SearchBar";
import PageHeader from "../../components/common/PageHeader";
import PageCard from "../../components/common/PageCard";
import ConfirmDialog from "../../components/common/ConfirmDialog";

import {
  getCoursesApi,
  deleteCourseApi,
} from "../../api/courseApi";

import type { Course } from "../../types/course";

import CreateCourseDialog from "./CreateCourseDialog";
import EditCourseDialog from "./EditCourseDialog";
import CourseDetailsDialog from "./CourseDetailsDialog";

export default function CoursesPage() {
  const [courses, setCourses] =
    useState<Course[]>([]);

  const [loading, setLoading] =
    useState(false);

  const [search, setSearch] =
    useState("");

  const [openCreate, setOpenCreate] =
    useState(false);

  const [openEdit, setOpenEdit] =
    useState(false);

  const [openDetails, setOpenDetails] =
    useState(false);

  const [selectedCourseId, setSelectedCourseId] =
    useState<number | null>(null);

  const [deleteCourseId, setDeleteCourseId] =
    useState<number | null>(null);

  const [confirmDelete, setConfirmDelete] =
    useState(false);

  const fetchCourses = useCallback(async () => {
    try {
      setLoading(true);

      const response =
        await getCoursesApi(
          1,
          20,
          search
        );

      setCourses(
        response.Data?.items ?? []
      );
    } catch {
      toast.error(
        "Failed to load courses"
      );
    } finally {
      setLoading(false);
    }
  }, [search]);

  useEffect(() => {
    const loadCourses = async () => {
      try {
        setLoading(true);

        const response =
          await getCoursesApi(
            1,
            20,
            search
          );

        setCourses(
          response.Data?.items ?? []
        );
      } catch {
        toast.error(
          "Failed to load courses"
        );
      } finally {
        setLoading(false);
      }
    };

    void loadCourses();
  }, [search]);

  const handleDelete = async () => {
    if (!deleteCourseId) {
      return;
    }

    try {
      await deleteCourseApi(
        deleteCourseId
      );

      toast.success(
        "Course deleted successfully"
      );

      setConfirmDelete(false);

      await fetchCourses();
    } catch {
      toast.error(
        "Failed to delete course"
      );
    }
  };

  const columns: GridColDef[] = [
    {
      field: "courseCode",
      headerName: "Course Code",
      flex: 1,
    },
    {
      field: "courseName",
      headerName: "Course Name",
      flex: 2,
    },
    {
      field: "credits",
      headerName: "Credits",
      width: 120,
    },
    {
      field: "actions",
      headerName: "Actions",
      width: 180,
      sortable: false,

      renderCell: (params) => (
        <Box>
          <IconButton
            onClick={() => {
              setSelectedCourseId(
                params.row.courseId
              );

              setOpenDetails(true);
            }}
          >
            <VisibilityOutlined />
          </IconButton>

          <IconButton
            onClick={() => {
              setSelectedCourseId(
                params.row.courseId
              );

              setOpenEdit(true);
            }}
          >
            <EditOutlined />
          </IconButton>

          <IconButton
            color="error"
            onClick={() => {
              setDeleteCourseId(
                params.row.courseId
              );

              setConfirmDelete(true);
            }}
          >
            <DeleteOutlined />
          </IconButton>
        </Box>
      ),
    },
  ];

  return (
    <Box>
      <PageHeader
        title="Courses"
        subtitle="Manage all courses"
        action={
          <Button
            variant="contained"
            startIcon={<AddOutlined />}
            onClick={() =>
              setOpenCreate(true)
            }
          >
            Add Course
          </Button>
        }
      />

      <Box sx={{ mb: 3 }}>
        <SearchBar
          value={search}
          onChange={setSearch}
          placeholder="Search courses..."
        />
      </Box>

      <PageCard>
        <Box
          sx={{
            height: 600,
          }}
        >
          <DataGrid
            rows={courses}
            columns={columns}
            loading={loading}
            getRowId={(row) =>
              row.courseId
            }
            disableRowSelectionOnClick
            pageSizeOptions={[
              10,
              20,
              50,
            ]}
            sx={{
              border: "none",

              "& .MuiDataGrid-columnHeaders":
                {
                  backgroundColor:
                    "#f9fafb",
                },

              "& .MuiDataGrid-row:hover":
                {
                  backgroundColor:
                    "#fffaf5",
                },
            }}
          />
        </Box>
      </PageCard>

      <CreateCourseDialog
        open={openCreate}
        onClose={() =>
          setOpenCreate(false)
        }
        onSuccess={fetchCourses}
      />

      <EditCourseDialog
        open={openEdit}
        courseId={selectedCourseId}
        onClose={() =>
          setOpenEdit(false)
        }
        onSuccess={fetchCourses}
      />

      <CourseDetailsDialog
        open={openDetails}
        courseId={selectedCourseId}
        onClose={() =>
          setOpenDetails(false)
        }
      />

      <ConfirmDialog
        open={confirmDelete}
        title="Delete Course"
        message="Are you sure you want to delete this course?"
        onConfirm={handleDelete}
        onClose={() =>
          setConfirmDelete(false)
        }
      />
    </Box>
  );
}