import { AddOutlined, DeleteOutlined } from "@mui/icons-material";
import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, IconButton, Tab, Tabs } from "@mui/material";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { useCallback, useState } from "react";
import toast from "react-hot-toast";
import { getCoursesApi } from "../../api/courseApi";
import { getEmployeesApi } from "../../api/employeeApi";
import { assignTeacherCourseApi, getCourseTeachersApi, getTeacherCoursesApi, unassignTeacherCourseApi } from "../../api/teacherCourseApi";
import ConfirmDialog from "../../components/common/ConfirmDialog";
import EmptyState from "../../components/common/EmptyState";
import EntityAutocomplete from "../../components/common/EntityAutocomplete";
import PageCard from "../../components/common/PageCard";
import PageHeader from "../../components/common/PageHeader";
import RequirePermission from "../../components/common/RequirePermission";
import { useHasPermission } from "../permissions/useHasPermission";
import type { Course } from "../../types/course";
import type { Employee } from "../../types/employee";
import type { TeacherCourse } from "../../types/teacherCourse";

const unwrap = <T,>(response: { data?: T } | T): T => (response as { data?: T }).data ?? response as T;
const date = (value: string) => value ? new Date(value).toLocaleDateString() : "—";

export default function TeacherCoursesPage() {
  const canView = useHasPermission("ViewTeacherCourse");
  const [tab, setTab] = useState(0);
  const [course, setCourse] = useState<Course | null>(null);
  const [employee, setEmployee] = useState<Employee | null>(null);
  const [assignments, setAssignments] = useState<TeacherCourse[]>([]);
  const [teacherCourses, setTeacherCourses] = useState<TeacherCourse[]>([]);
  const [loading, setLoading] = useState(false);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [selectedEmployee, setSelectedEmployee] = useState<Employee | null>(null);
  const [unassignTarget, setUnassignTarget] = useState<TeacherCourse | null>(null);
  const loadCourses = useCallback(async (search: string) => { const page = unwrap(await getCoursesApi(1, 100, search)) as { items?: Course[] }; return page.items ?? []; }, []);
  const loadEmployees = useCallback(async (search: string) => { const page = unwrap(await getEmployeesApi(1, 100, search)) as { items?: Employee[] }; return page.items ?? []; }, []);
  const excludeAssignedEmployees = useCallback((item: Employee) => assignments.some((entry) => entry.employeeId === item.employeeId), [assignments]);
  const loadAssignments = useCallback(async (courseId: number) => { try { setLoading(true); setAssignments(unwrap(await getCourseTeachersApi(courseId)) as TeacherCourse[]); } catch { toast.error("Failed to load course teachers"); } finally { setLoading(false); } }, []);
  const loadTeacherCourses = useCallback(async (employeeId: number) => { try { setLoading(true); setTeacherCourses(unwrap(await getTeacherCoursesApi(employeeId)) as TeacherCourse[]); } catch { toast.error("Failed to load teacher courses"); } finally { setLoading(false); } }, []);
  const assign = async () => { if (!course || !selectedEmployee) return; try { await assignTeacherCourseApi(selectedEmployee.employeeId, course.courseId); toast.success("Teacher assigned successfully"); setDialogOpen(false); setSelectedEmployee(null); await loadAssignments(course.courseId); } catch { toast.error("Failed to assign teacher"); } };
  const unassign = async () => { if (!unassignTarget) return; try { await unassignTeacherCourseApi(unassignTarget.employeeId, unassignTarget.courseId); toast.success("Teacher unassigned successfully"); setUnassignTarget(null); if (course) await loadAssignments(course.courseId); } catch { toast.error("Failed to unassign teacher"); } };
  const columns: GridColDef<TeacherCourse>[] = [{ field: "employeeName", headerName: "Teacher", flex: 1.5 }, { field: "empCode", headerName: "Employee Code", flex: 1 }, { field: "assignedAt", headerName: "Assigned At", flex: 1, valueGetter: (_v, row) => date(row.assignedAt) }, { field: "actions", headerName: "Actions", width: 100, sortable: false, renderCell: (params) => <RequirePermission name="AssignTeacherCourse"><IconButton color="error" aria-label="Unassign teacher" onClick={() => setUnassignTarget(params.row)}><DeleteOutlined /></IconButton></RequirePermission> }];
  const teacherColumns: GridColDef<TeacherCourse>[] = [{ field: "courseCode", headerName: "Course Code", flex: 1 }, { field: "courseName", headerName: "Course", flex: 1.5 }, { field: "assignedAt", headerName: "Assigned At", flex: 1, valueGetter: (_v, row) => date(row.assignedAt) }];
  if (!canView) return <Box><PageHeader title="Teacher Courses" subtitle="Manage teacher course assignments" /><PageCard><EmptyState title="No access" subtitle="You do not have permission to view teacher course assignments." /></PageCard></Box>;
  return <Box><PageHeader title="Teacher Courses" subtitle="Manage teacher course assignments" /><Tabs value={tab} onChange={(_event, value) => setTab(value)} sx={{ mb: 3 }}><Tab label="By Course" /><Tab label="By Teacher" /></Tabs>
    {tab === 0 ? <><Box sx={{ display: "flex", gap: 2, alignItems: "center", mb: 3, maxWidth: 720 }}><Box sx={{ flex: 1 }}><EntityAutocomplete label="Select course" value={course} onChange={(value) => { setCourse(value); setAssignments([]); if (value) void loadAssignments(value.courseId); }} loadOptions={loadCourses} getOptionKey={(item) => item.courseId} getOptionLabel={(item) => `${item.courseCode} — ${item.courseName}`} /></Box><RequirePermission name="AssignTeacherCourse"><Button variant="contained" startIcon={<AddOutlined />} disabled={!course} onClick={() => setDialogOpen(true)}>Assign teacher</Button></RequirePermission></Box><PageCard><Box sx={{ height: 480 }}><DataGrid rows={assignments} columns={columns} loading={loading} getRowId={(row) => `${row.employeeId}-${row.courseId}`} disableRowSelectionOnClick /></Box></PageCard></> : <><Box sx={{ mb: 3, maxWidth: 520 }}><EntityAutocomplete label="Select teacher" value={employee} onChange={(value) => { setEmployee(value); setTeacherCourses([]); if (value) void loadTeacherCourses(value.employeeId); }} loadOptions={loadEmployees} getOptionKey={(item) => item.employeeId} getOptionLabel={(item) => `${[item.firstName, item.lastName].filter(Boolean).join(" ") || item.email || item.empCode} (${item.empCode})`} /></Box><PageCard><Box sx={{ height: 480 }}><DataGrid rows={teacherCourses} columns={teacherColumns} loading={loading} getRowId={(row) => `${row.employeeId}-${row.courseId}`} disableRowSelectionOnClick /></Box></PageCard></>}
    <Dialog open={dialogOpen} onClose={() => setDialogOpen(false)} fullWidth maxWidth="sm"><DialogTitle>Assign teacher</DialogTitle><DialogContent sx={{ pt: "16px !important" }}><EntityAutocomplete label="Teacher" value={selectedEmployee} onChange={(value) => setSelectedEmployee(value)} loadOptions={loadEmployees} exclude={excludeAssignedEmployees} getOptionKey={(item) => item.employeeId} getOptionLabel={(item) => `${[item.firstName, item.lastName].filter(Boolean).join(" ") || item.email || item.empCode} (${item.empCode})`} /></DialogContent><DialogActions><Button onClick={() => setDialogOpen(false)}>Cancel</Button><Button variant="contained" disabled={!selectedEmployee} onClick={() => void assign()}>Assign</Button></DialogActions></Dialog>
    <ConfirmDialog open={!!unassignTarget} title="Unassign teacher" message="Remove this teacher from the course?" onClose={() => setUnassignTarget(null)} onConfirm={() => void unassign()} />
  </Box>;
}
