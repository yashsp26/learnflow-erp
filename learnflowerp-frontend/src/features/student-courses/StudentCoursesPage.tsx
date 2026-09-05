import { AddOutlined, DeleteOutlined } from "@mui/icons-material";
import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, IconButton, Tab, Tabs } from "@mui/material";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { useCallback, useState } from "react";
import toast from "react-hot-toast";
import { assignStudentCourseApi, getCourseStudentsApi, getStudentCoursesApi, removeStudentCourseApi } from "../../api/studentCourseApi";
import { getCoursesApi } from "../../api/courseApi";
import { getStudentsApi } from "../../api/studentApi";
import EntityAutocomplete from "../../components/common/EntityAutocomplete";
import ConfirmDialog from "../../components/common/ConfirmDialog";
import EmptyState from "../../components/common/EmptyState";
import PageCard from "../../components/common/PageCard";
import PageHeader from "../../components/common/PageHeader";
import RequirePermission from "../../components/common/RequirePermission";
import { useHasPermission } from "../permissions/useHasPermission";
import type { Course } from "../../types/course";
import type { Student } from "../../types/student";
import type { StudentCourse } from "../../types/studentCourse";

const unwrap = <T,>(response: { data?: T } | T): T => (response as { data?: T }).data ?? response as T;
const date = (value: string) => value ? new Date(value).toLocaleDateString() : "—";

export default function StudentCoursesPage() {
  const canViewCourse = useHasPermission("ViewCourse");
  const canViewStudent = useHasPermission("ViewStudent");
  const [tab, setTab] = useState(0);
  const [course, setCourse] = useState<Course | null>(null);
  const [student, setStudent] = useState<Student | null>(null);
  const [roster, setRoster] = useState<StudentCourse[]>([]);
  const [enrollments, setEnrollments] = useState<StudentCourse[]>([]);
  const [loading, setLoading] = useState(false);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [enrollStudent, setEnrollStudent] = useState<Student | null>(null);
  const [removeTarget, setRemoveTarget] = useState<StudentCourse | null>(null);

  const loadCourses = useCallback(async (search: string) => {
    const response = unwrap(await getCoursesApi(1, 100, search)) as { items?: Course[] };
    return response.items ?? [];
  }, []);
  const loadStudents = useCallback(async (search: string) => {
    const response = unwrap(await getStudentsApi(1, 100, search)) as { items?: Student[] };
    return response.items ?? [];
  }, []);
  const excludeEnrolledStudents = useCallback((item: Student) => roster.some((entry) => entry.studentId === item.studentId), [roster]);
  const loadRoster = useCallback(async (courseId: number) => {
    try { setLoading(true); setRoster(unwrap(await getCourseStudentsApi(courseId)) as StudentCourse[]); }
    catch { toast.error("Failed to load course roster"); } finally { setLoading(false); }
  }, []);
  const loadEnrollments = useCallback(async (studentId: number) => {
    try { setLoading(true); setEnrollments(unwrap(await getStudentCoursesApi(studentId)) as StudentCourse[]); }
    catch { toast.error("Failed to load student courses"); } finally { setLoading(false); }
  }, []);
  const selectCourse = (value: Course | null) => { setCourse(value); setRoster([]); if (value) void loadRoster(value.courseId); };
  const selectStudent = (value: Student | null) => { setStudent(value); setEnrollments([]); if (value) void loadEnrollments(value.studentId); };
  const enroll = async () => {
    if (!course || !enrollStudent) return;
    try { await assignStudentCourseApi(enrollStudent.studentId, course.courseId); toast.success("Student enrolled successfully"); setDialogOpen(false); setEnrollStudent(null); await loadRoster(course.courseId); }
    catch { toast.error("Failed to enroll student"); }
  };
  const remove = async () => {
    if (!removeTarget) return;
    try { await removeStudentCourseApi(removeTarget.studentId, removeTarget.courseId); toast.success("Student removed from course"); setRemoveTarget(null); if (course) await loadRoster(course.courseId); }
    catch { toast.error("Failed to remove student"); }
  };
  const rosterColumns: GridColDef<StudentCourse>[] = [
    { field: "studentName", headerName: "Student", flex: 1.5 },
    { field: "enrollmentDate", headerName: "Enrollment Date", flex: 1, valueGetter: (_v, row) => date(row.enrollmentDate) },
    { field: "status", headerName: "Status", flex: 1 },
    { field: "actions", headerName: "Actions", width: 100, sortable: false, renderCell: (params) => <RequirePermission name="AssignCourse"><IconButton color="error" aria-label="Remove student" onClick={() => setRemoveTarget(params.row)}><DeleteOutlined /></IconButton></RequirePermission> },
  ];
  const enrollmentColumns: GridColDef<StudentCourse>[] = [
    { field: "courseName", headerName: "Course", flex: 1.5 },
    { field: "enrollmentDate", headerName: "Enrollment Date", flex: 1, valueGetter: (_v, row) => date(row.enrollmentDate) },
    { field: "status", headerName: "Status", flex: 1 },
  ];
  const noAccess = (label: string) => <PageCard><EmptyState title="No access" subtitle={`You do not have permission to view ${label}.`} /></PageCard>;

  return <Box>
    <PageHeader title="Student Courses" subtitle="Manage student course enrollments" />
    <Tabs value={tab} onChange={(_event, value) => setTab(value)} sx={{ mb: 3 }}><Tab label="By Course" /><Tab label="By Student" /></Tabs>
    {tab === 0 && (canViewCourse ? <>
      <Box sx={{ display: "flex", gap: 2, alignItems: "center", mb: 3, maxWidth: 720 }}><Box sx={{ flex: 1 }}><EntityAutocomplete label="Select course" value={course} onChange={selectCourse} loadOptions={loadCourses} getOptionKey={(item) => item.courseId} getOptionLabel={(item) => `${item.courseCode} — ${item.courseName}`} /></Box><RequirePermission name="AssignCourse"><Button variant="contained" startIcon={<AddOutlined />} disabled={!course} onClick={() => setDialogOpen(true)}>Enroll student</Button></RequirePermission></Box>
      <PageCard><Box sx={{ height: 480 }}><DataGrid rows={roster} columns={rosterColumns} loading={loading} getRowId={(row) => `${row.studentId}-${row.courseId}`} disableRowSelectionOnClick /></Box></PageCard>
    </> : noAccess("course rosters"))}
    {tab === 1 && (canViewStudent ? <><Box sx={{ mb: 3, maxWidth: 520 }}><EntityAutocomplete label="Select student" value={student} onChange={selectStudent} loadOptions={loadStudents} getOptionKey={(item) => item.studentId} getOptionLabel={(item) => `${[item.firstName, item.lastName].filter(Boolean).join(" ") || item.email} (${item.enrollmentNo})`} /></Box><PageCard><Box sx={{ height: 480 }}><DataGrid rows={enrollments} columns={enrollmentColumns} loading={loading} getRowId={(row) => `${row.studentId}-${row.courseId}`} disableRowSelectionOnClick /></Box></PageCard></> : noAccess("student enrollments"))}
    <Dialog open={dialogOpen} onClose={() => setDialogOpen(false)} fullWidth maxWidth="sm"><DialogTitle>Enroll student</DialogTitle><DialogContent sx={{ pt: "16px !important" }}><EntityAutocomplete label="Student" value={enrollStudent} onChange={(value) => setEnrollStudent(value)} loadOptions={loadStudents} exclude={excludeEnrolledStudents} getOptionKey={(item) => item.studentId} getOptionLabel={(item) => `${[item.firstName, item.lastName].filter(Boolean).join(" ") || item.email} (${item.enrollmentNo})`} /></DialogContent><DialogActions><Button onClick={() => setDialogOpen(false)}>Cancel</Button><Button variant="contained" onClick={() => void enroll()} disabled={!enrollStudent}>Enroll</Button></DialogActions></Dialog>
    <ConfirmDialog open={!!removeTarget} title="Remove student" message="Remove this student from the course?" onClose={() => setRemoveTarget(null)} onConfirm={() => void remove()} />
  </Box>;
}
