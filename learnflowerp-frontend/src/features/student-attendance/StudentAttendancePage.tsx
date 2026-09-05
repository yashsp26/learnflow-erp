import { Box, Button, FormControl, InputLabel, MenuItem, Select, Tab, Tabs, Typography } from "@mui/material";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { useCallback, useEffect, useState } from "react";
import toast from "react-hot-toast";
import { getCoursesApi } from "../../api/courseApi";
import { getStudentsApi } from "../../api/studentApi";
import { getCourseStudentsApi } from "../../api/studentCourseApi";
import { getCourseAttendanceApi, getTodayStudentAttendanceApi, getStudentAttendanceApi, markStudentAttendanceApi } from "../../api/studentAttendanceApi";
import EntityAutocomplete from "../../components/common/EntityAutocomplete";
import EmptyState from "../../components/common/EmptyState";
import PageCard from "../../components/common/PageCard";
import PageHeader from "../../components/common/PageHeader";
import RequirePermission from "../../components/common/RequirePermission";
import StatusChip from "../../components/common/StatusChip";
import type { AttendanceStatus, StudentAttendanceHistory, StudentAttendanceRecord } from "../../types/attendance";
import { attendanceStatuses } from "../../types/attendance";
import type { Course } from "../../types/course";
import type { Student } from "../../types/student";
import type { StudentCourse } from "../../types/studentCourse";
import { useHasPermission } from "../permissions/useHasPermission";

type MarkedStudent = StudentCourse & { status: AttendanceStatus };
const unwrap = <T,>(response: { data?: T } | T): T => (response as { data?: T }).data ?? response as T;
const formatDate = (value: string) => value ? new Date(value).toLocaleDateString() : "—";

const StatusSelect = ({ value, onChange }: { value: AttendanceStatus; onChange: (value: AttendanceStatus) => void }) => <FormControl size="small" fullWidth><InputLabel>Status</InputLabel><Select label="Status" value={value} onChange={(event) => onChange(event.target.value as AttendanceStatus)}>{attendanceStatuses.map((status) => <MenuItem key={status} value={status}>{status}</MenuItem>)}</Select></FormControl>;

export default function StudentAttendancePage() {
  const canMark = useHasPermission("MarkStudentAttendance");
  const canView = useHasPermission("ViewStudentAttendance");
  const tabs = [canMark && "mark", canView && "today", canView && "history"].filter(Boolean) as string[];
  const [selectedTab, setSelectedTab] = useState("mark");
  const activeTab = tabs.includes(selectedTab) ? selectedTab : (tabs[0] ?? "none");
  const [course, setCourse] = useState<Course | null>(null);
  const [markRoster, setMarkRoster] = useState<MarkedStudent[]>([]);
  const [courseRecords, setCourseRecords] = useState<StudentAttendanceRecord[]>([]);
  const [today, setToday] = useState<StudentAttendanceRecord[]>([]);
  const [todayCourse, setTodayCourse] = useState<Course | null>(null);
  const [todayRoster, setTodayRoster] = useState<StudentCourse[]>([]);
  const [student, setStudent] = useState<Student | null>(null);
  const [history, setHistory] = useState<StudentAttendanceHistory[]>([]);
  const [loading, setLoading] = useState(false);
  const loadCourses = useCallback(async (search: string) => { const page = unwrap(await getCoursesApi(1, 100, search)) as { items?: Course[] }; return page.items ?? []; }, []);
  const loadStudents = useCallback(async (search: string) => { const page = unwrap(await getStudentsApi(1, 100, search)) as { items?: Student[] }; return page.items ?? []; }, []);
  const loadRoster = useCallback(async (courseId: number) => { const roster = unwrap(await getCourseStudentsApi(courseId)) as StudentCourse[]; return roster; }, []);
  const selectCourse = async (value: Course | null) => { setCourse(value); setMarkRoster([]); setCourseRecords([]); if (!value) return; try { setLoading(true); const [roster, records] = await Promise.all([loadRoster(value.courseId), getCourseAttendanceApi(value.courseId)]); setMarkRoster(roster.map((item) => ({ ...item, status: "Present" }))); setCourseRecords(unwrap(records) as StudentAttendanceRecord[]); } catch { toast.error("Failed to load course attendance"); } finally { setLoading(false); } };
  const updateRosterStatus = (studentId: number, status: AttendanceStatus) => setMarkRoster((current) => current.map((item) => item.studentId === studentId ? { ...item, status } : item));
  const markAttendance = async () => { if (!course || markRoster.length === 0) return; try { await markStudentAttendanceApi(course.courseId, markRoster.map(({ studentId, status }) => ({ studentId, status }))); toast.success("Attendance marked successfully"); } catch { toast.error("Failed to mark attendance"); } };
  const loadToday = useCallback(async () => { try { setLoading(true); setToday(unwrap(await getTodayStudentAttendanceApi()) as StudentAttendanceRecord[]); } catch { toast.error("Failed to load today's attendance"); } finally { setLoading(false); } }, []);
  useEffect(() => { if (activeTab === "today") void loadToday(); }, [activeTab, loadToday]);
  const selectTodayCourse = async (value: Course | null) => { setTodayCourse(value); setTodayRoster([]); if (!value) return; try { setTodayRoster(await loadRoster(value.courseId)); } catch { toast.error("Failed to load course roster"); } };
  const selectStudent = async (value: Student | null) => { setStudent(value); setHistory([]); if (!value) return; try { setLoading(true); setHistory(unwrap(await getStudentAttendanceApi(value.studentId)) as StudentAttendanceHistory[]); } catch { toast.error("Failed to load student attendance"); } finally { setLoading(false); } };
  const markColumns: GridColDef<MarkedStudent>[] = [{ field: "studentName", headerName: "Student", flex: 1.5 }, { field: "status", headerName: "Status", width: 190, renderCell: (params) => <StatusSelect value={params.row.status} onChange={(status) => updateRosterStatus(params.row.studentId, status)} /> }];
  const todayRows = todayCourse ? today.filter((item) => item.courseId === todayCourse.courseId) : today;
  const todayColumns: GridColDef<StudentAttendanceRecord>[] = [{ field: "studentId", headerName: "Student", flex: 1.5, valueGetter: (_v, row) => todayRoster.find((item) => item.studentId === row.studentId)?.studentName ?? `Student #${row.studentId}` }, { field: "courseId", headerName: "Course ID", flex: 1 }, { field: "attendanceDate", headerName: "Date", flex: 1, valueGetter: (_v, row) => formatDate(row.attendanceDate) }, { field: "status", headerName: "Status", width: 130, renderCell: (params) => <StatusChip status={params.row.status} /> }];
  const historyColumns: GridColDef<StudentAttendanceHistory>[] = [{ field: "date", headerName: "Date", flex: 1, valueGetter: (_v, row) => formatDate(row.date) }, { field: "courseName", headerName: "Course", flex: 1.5 }, { field: "status", headerName: "Status", width: 130, renderCell: (params) => <StatusChip status={params.row.status} /> }];
  if (!tabs.length) return <Box><PageHeader title="Student Attendance" subtitle="Record and review student attendance" /><PageCard><EmptyState title="No access" subtitle="You do not have permission to view or mark student attendance." /></PageCard></Box>;
  return <Box><PageHeader title="Student Attendance" subtitle="Record and review student attendance" /><Tabs value={activeTab} onChange={(_event, value) => setSelectedTab(value)} sx={{ mb: 3 }}>{canMark && <Tab value="mark" label="Mark Attendance" />}{canView && <Tab value="today" label="Today" />}{canView && <Tab value="history" label="Student History" />}</Tabs>
    {activeTab === "mark" && <RequirePermission name="MarkStudentAttendance"><Box sx={{ maxWidth: 560, mb: 3 }}><EntityAutocomplete label="Select course" value={course} onChange={(value) => void selectCourse(value)} loadOptions={loadCourses} getOptionKey={(item) => item.courseId} getOptionLabel={(item) => `${item.courseCode} — ${item.courseName}`} /></Box>{course && <Typography color="text.secondary" sx={{ mb: 2 }}>{courseRecords.filter((item) => new Date(item.attendanceDate).toDateString() === new Date().toDateString()).length} students already have attendance recorded today for this course.</Typography>}<Box sx={{ display: "flex", gap: 1, mb: 2 }}><Button onClick={() => setMarkRoster((items) => items.map((item) => ({ ...item, status: "Present" })))} disabled={!markRoster.length}>Mark all present</Button><Button onClick={() => setMarkRoster((items) => items.map((item) => ({ ...item, status: "Absent" })))} disabled={!markRoster.length}>Mark all absent</Button><Button variant="contained" onClick={() => void markAttendance()} disabled={!course || !markRoster.length}>Submit attendance</Button></Box><PageCard><Box sx={{ height: 460 }}><DataGrid rows={markRoster} columns={markColumns} loading={loading} getRowId={(row) => row.studentId} disableRowSelectionOnClick /></Box></PageCard></RequirePermission>}
    {activeTab === "today" && <Box><Box sx={{ maxWidth: 560, mb: 3 }}><EntityAutocomplete label="Filter by course (optional)" value={todayCourse} onChange={(value) => void selectTodayCourse(value)} loadOptions={loadCourses} getOptionKey={(item) => item.courseId} getOptionLabel={(item) => `${item.courseCode} — ${item.courseName}`} /></Box><PageCard><Box sx={{ height: 460 }}><DataGrid rows={todayRows} columns={todayColumns} loading={loading} getRowId={(row) => row.studentAttendanceId} disableRowSelectionOnClick /></Box></PageCard></Box>}
    {activeTab === "history" && <Box><Box sx={{ maxWidth: 560, mb: 3 }}><EntityAutocomplete label="Select student" value={student} onChange={(value) => void selectStudent(value)} loadOptions={loadStudents} getOptionKey={(item) => item.studentId} getOptionLabel={(item) => `${[item.firstName, item.lastName].filter(Boolean).join(" ") || item.email} (${item.enrollmentNo})`} /></Box><PageCard><Box sx={{ height: 460 }}><DataGrid rows={history} columns={historyColumns} loading={loading} getRowId={(row) => `${row.date}-${row.courseName}`} disableRowSelectionOnClick /></Box></PageCard></Box>}
  </Box>;
}
