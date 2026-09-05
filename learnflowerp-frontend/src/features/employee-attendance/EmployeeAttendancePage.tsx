import { Box, Button, FormControl, InputLabel, MenuItem, Select, Tab, Tabs } from "@mui/material";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { useCallback, useEffect, useState } from "react";
import toast from "react-hot-toast";
import { getEmployeesApi } from "../../api/employeeApi";
import { getEmployeeAttendanceApi, getTodayEmployeeAttendanceApi, markEmployeeAttendanceApi } from "../../api/employeeAttendanceApi";
import EntityAutocomplete from "../../components/common/EntityAutocomplete";
import EmptyState from "../../components/common/EmptyState";
import PageCard from "../../components/common/PageCard";
import PageHeader from "../../components/common/PageHeader";
import RequirePermission from "../../components/common/RequirePermission";
import StatusChip from "../../components/common/StatusChip";
import { attendanceStatuses, type AttendanceStatus, type EmployeeAttendance } from "../../types/attendance";
import type { Employee } from "../../types/employee";
import { useHasPermission } from "../permissions/useHasPermission";

const unwrap = <T,>(response: { data?: T } | T): T => (response as { data?: T }).data ?? response as T;
const formatDate = (value: string) => value ? new Date(value).toLocaleDateString() : "—";

export default function EmployeeAttendancePage() {
  const canMark = useHasPermission("MarkEmployeeAttendance");
  const canView = useHasPermission("ViewEmployeeAttendance");
  const tabs = [canMark && "mark", canView && "view"].filter(Boolean) as string[];
  const [selectedTab, setSelectedTab] = useState("mark");
  const activeTab = tabs.includes(selectedTab) ? selectedTab : (tabs[0] ?? "none");
  const [employee, setEmployee] = useState<Employee | null>(null);
  const [status, setStatus] = useState<AttendanceStatus>("Present");
  const [today, setToday] = useState<EmployeeAttendance[]>([]);
  const [historyEmployee, setHistoryEmployee] = useState<Employee | null>(null);
  const [history, setHistory] = useState<EmployeeAttendance[]>([]);
  const [loading, setLoading] = useState(false);
  const loadEmployees = useCallback(async (search: string) => { const page = unwrap(await getEmployeesApi(1, 100, search)) as { items?: Employee[] }; return page.items ?? []; }, []);
  const mark = async () => { if (!employee) return; try { await markEmployeeAttendanceApi(employee.employeeId, status); toast.success("Attendance marked successfully"); } catch { toast.error("Failed to mark attendance"); } };
  const loadToday = useCallback(async () => { try { setLoading(true); setToday(unwrap(await getTodayEmployeeAttendanceApi()) as EmployeeAttendance[]); } catch { toast.error("Failed to load today's attendance"); } finally { setLoading(false); } }, []);
  useEffect(() => { if (activeTab === "view") void loadToday(); }, [activeTab, loadToday]);
  const selectHistoryEmployee = async (value: Employee | null) => { setHistoryEmployee(value); setHistory([]); if (!value) return; try { setLoading(true); setHistory(unwrap(await getEmployeeAttendanceApi(value.employeeId)) as EmployeeAttendance[]); } catch { toast.error("Failed to load employee attendance history"); } finally { setLoading(false); } };
  const columns: GridColDef<EmployeeAttendance>[] = [{ field: "employeeName", headerName: "Employee", flex: 1.5 }, { field: "attendanceDate", headerName: "Date", flex: 1, valueGetter: (_v, row) => formatDate(row.attendanceDate) }, { field: "status", headerName: "Status", width: 130, renderCell: (params) => <StatusChip status={params.row.status} /> }];
  const label = (item: Employee) => `${[item.firstName, item.lastName].filter(Boolean).join(" ") || item.email || item.empCode} (${item.empCode})`;
  if (!tabs.length) return <Box><PageHeader title="Employee Attendance" subtitle="Record and review employee attendance" /><PageCard><EmptyState title="No access" subtitle="You do not have permission to view or mark employee attendance." /></PageCard></Box>;
  return <Box><PageHeader title="Employee Attendance" subtitle="Record and review employee attendance" /><Tabs value={activeTab} onChange={(_event, value) => setSelectedTab(value)} sx={{ mb: 3 }}>{canMark && <Tab value="mark" label="Mark Attendance" />}{canView && <Tab value="view" label="Today / History" />}</Tabs>
    {activeTab === "mark" && <RequirePermission name="MarkEmployeeAttendance"><PageCard><Box sx={{ display: "flex", flexDirection: { xs: "column", md: "row" }, gap: 2, alignItems: { md: "center" }, maxWidth: 900 }}><Box sx={{ flex: 1 }}><EntityAutocomplete label="Select employee" value={employee} onChange={(value) => setEmployee(value)} loadOptions={loadEmployees} getOptionKey={(item) => item.employeeId} getOptionLabel={label} /></Box><FormControl sx={{ minWidth: 150 }}><InputLabel>Status</InputLabel><Select label="Status" value={status} onChange={(event) => setStatus(event.target.value as AttendanceStatus)}>{attendanceStatuses.map((item) => <MenuItem key={item} value={item}>{item}</MenuItem>)}</Select></FormControl><Button variant="contained" onClick={() => void mark()} disabled={!employee}>Mark attendance</Button></Box></PageCard></RequirePermission>}
    {activeTab === "view" && <Box><PageCard><Box sx={{ height: 360 }}><DataGrid rows={today} columns={columns} loading={loading} getRowId={(row) => `${row.employeeId}-${row.attendanceDate}`} disableRowSelectionOnClick /></Box></PageCard><Box sx={{ maxWidth: 560, mt: 4, mb: 2 }}><EntityAutocomplete label="Select employee for history" value={historyEmployee} onChange={(value) => void selectHistoryEmployee(value)} loadOptions={loadEmployees} getOptionKey={(item) => item.employeeId} getOptionLabel={label} /></Box><PageCard><Box sx={{ height: 360 }}><DataGrid rows={history} columns={columns} loading={loading} getRowId={(row) => `${row.employeeId}-${row.attendanceDate}`} disableRowSelectionOnClick /></Box></PageCard></Box>}
  </Box>;
}
