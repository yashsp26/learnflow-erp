import { AddOutlined } from "@mui/icons-material";
import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControl, InputLabel, MenuItem, Select, Tab, Tabs, TextField, Typography } from "@mui/material";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { useCallback, useEffect, useMemo, useState } from "react";
import toast from "react-hot-toast";
import { createFeeApi, getFeesApi, getStudentFeesApi } from "../../api/feeApi";
import { getStudentsApi } from "../../api/studentApi";
import EntityAutocomplete from "../../components/common/EntityAutocomplete";
import EmptyState from "../../components/common/EmptyState";
import PageCard from "../../components/common/PageCard";
import PageHeader from "../../components/common/PageHeader";
import RequirePermission from "../../components/common/RequirePermission";
import StatusChip from "../../components/common/StatusChip";
import type { Fee, FeeStatus } from "../../types/fee";
import { feeStatuses, feeTypes } from "../../types/fee";
import type { Student } from "../../types/student";
import { useHasPermission } from "../permissions/useHasPermission";

const unwrap = <T,>(response: { data?: T } | T): T => (response as { data?: T }).data ?? response as T;
const money = (amount: number) => new Intl.NumberFormat("en-IN", { style: "currency", currency: "INR" }).format(amount);
const date = (value: string | null) => value ? new Date(value).toLocaleDateString() : "—";
const studentLabel = (student: Student) => `${[student.firstName, student.lastName].filter(Boolean).join(" ") || student.email} (${student.enrollmentNo})`;

export default function FeesPage() {
  const canView = useHasPermission("ViewFee");
  const [tab, setTab] = useState("all");
  const [fees, setFees] = useState<Fee[]>([]);
  const [students, setStudents] = useState<Student[]>([]);
  const [loading, setLoading] = useState(false);
  const [search, setSearch] = useState("");
  const [statusFilter, setStatusFilter] = useState<FeeStatus | "">("");
  const [typeFilter, setTypeFilter] = useState("");
  const [yearFilter, setYearFilter] = useState("");
  const [byStudent, setByStudent] = useState<Student | null>(null);
  const [studentFees, setStudentFees] = useState<Fee[]>([]);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [details, setDetails] = useState<Fee | null>(null);
  const [formStudent, setFormStudent] = useState<Student | null>(null);
  const [totalAmount, setTotalAmount] = useState("");
  const [dueDate, setDueDate] = useState("");
  const [feeType, setFeeType] = useState<string>(feeTypes[0]);
  const [academicYear, setAcademicYear] = useState("");
  const loadStudents = useCallback(async (searchTerm: string) => { const page = unwrap(await getStudentsApi(1, 100, searchTerm)) as { items?: Student[] }; return page.items ?? []; }, []);
  const fetchFees = useCallback(async () => { try { setLoading(true); setFees(unwrap(await getFeesApi()) as Fee[]); } catch { toast.error("Failed to load fees"); } finally { setLoading(false); } }, []);
  useEffect(() => { if (canView) void fetchFees(); }, [canView, fetchFees]);
  useEffect(() => { if (!canView) return; void loadStudents("").then(setStudents).catch(() => toast.error("Failed to load students")); }, [canView, loadStudents]);
  const filteredFees = useMemo(() => fees.filter((fee) => {
    const student = students.find((item) => item.studentId === fee.studentId);
    const query = search.toLowerCase();
    return (!query || [student ? studentLabel(student) : "", fee.feeType, fee.academicYear].join(" ").toLowerCase().includes(query)) && (!statusFilter || fee.status === statusFilter) && (!typeFilter || fee.feeType === typeFilter) && (!yearFilter || fee.academicYear.toLowerCase().includes(yearFilter.toLowerCase()));
  }), [fees, students, search, statusFilter, typeFilter, yearFilter]);
  const selectStudent = async (value: Student | null) => { setByStudent(value); setStudentFees([]); if (!value) return; try { setLoading(true); setStudentFees(unwrap(await getStudentFeesApi(value.studentId)) as Fee[]); } catch { toast.error("Failed to load student fees"); } finally { setLoading(false); } };
  const createFee = async () => { const amount = Number(totalAmount); if (!formStudent || !amount || amount <= 0 || !dueDate || !academicYear.trim()) { toast.error("Complete all required fee fields"); return; } try { await createFeeApi(formStudent.studentId, amount, dueDate, feeType, academicYear.trim()); toast.success("Fee created successfully"); setDialogOpen(false); setFormStudent(null); setTotalAmount(""); setDueDate(""); setAcademicYear(""); await fetchFees(); } catch { toast.error("Failed to create fee"); } };
  const columns: GridColDef<Fee>[] = [{ field: "studentId", headerName: "Student", flex: 1.5, valueGetter: (_v, row) => { const student = students.find((item) => item.studentId === row.studentId); return student ? studentLabel(student) : `Student #${row.studentId}`; } }, { field: "feeType", headerName: "Fee Type", flex: 1 }, { field: "academicYear", headerName: "Academic Year", flex: 1 }, { field: "totalAmount", headerName: "Total", flex: 1, valueGetter: (_v, row) => money(row.totalAmount) }, { field: "paidAmount", headerName: "Paid", flex: 1, valueGetter: (_v, row) => money(row.paidAmount) }, { field: "pendingAmount", headerName: "Pending", flex: 1, valueGetter: (_v, row) => money(row.pendingAmount) }, { field: "dueDate", headerName: "Due Date", flex: 1, valueGetter: (_v, row) => date(row.dueDate) }, { field: "status", headerName: "Status", width: 125, renderCell: (params) => <StatusChip status={params.row.status} /> }];
  if (!canView) return <Box><PageHeader title="Fees" subtitle="Manage student fees" /><PageCard><EmptyState title="No access" subtitle="You do not have permission to view fees." /></PageCard></Box>;
  return <Box><PageHeader title="Fees" subtitle="Manage student fees" action={<RequirePermission name="CreateFee"><Button variant="contained" startIcon={<AddOutlined />} onClick={() => setDialogOpen(true)}>Create Fee</Button></RequirePermission>} /><Tabs value={tab} onChange={(_event, value) => setTab(value)} sx={{ mb: 3 }}><Tab value="all" label="All Fees" /><Tab value="student" label="By Student" /></Tabs>
    {tab === "all" && <><Box sx={{ display: "grid", gridTemplateColumns: { xs: "1fr", md: "2fr 1fr 1fr 1fr" }, gap: 2, mb: 3 }}><TextField label="Search student, fee type or year" value={search} onChange={(event) => setSearch(event.target.value)} /><FormControl><InputLabel>Status</InputLabel><Select label="Status" value={statusFilter} onChange={(event) => setStatusFilter(event.target.value as FeeStatus | "")}><MenuItem value="">All statuses</MenuItem>{feeStatuses.map((item) => <MenuItem key={item} value={item}>{item}</MenuItem>)}</Select></FormControl><FormControl><InputLabel>Fee type</InputLabel><Select label="Fee type" value={typeFilter} onChange={(event) => setTypeFilter(event.target.value)}><MenuItem value="">All types</MenuItem>{Array.from(new Set(fees.map((item) => item.feeType))).map((item) => <MenuItem key={item} value={item}>{item}</MenuItem>)}</Select></FormControl><TextField label="Academic year" value={yearFilter} onChange={(event) => setYearFilter(event.target.value)} /></Box><PageCard><Box sx={{ height: 560 }}><DataGrid rows={filteredFees} columns={columns} loading={loading} getRowId={(row) => row.feeId} onRowClick={(params) => setDetails(params.row)} disableRowSelectionOnClick pageSizeOptions={[10, 25, 50]} /></Box></PageCard></>}
    {tab === "student" && <><Box sx={{ maxWidth: 560, mb: 3 }}><EntityAutocomplete label="Select student" value={byStudent} onChange={(value) => void selectStudent(value)} loadOptions={loadStudents} getOptionKey={(item) => item.studentId} getOptionLabel={studentLabel} /></Box><PageCard><Box sx={{ height: 560 }}><DataGrid rows={studentFees} columns={columns} loading={loading} getRowId={(row) => row.feeId} onRowClick={(params) => setDetails(params.row)} disableRowSelectionOnClick pageSizeOptions={[10, 25, 50]} /></Box></PageCard></>}
    <Dialog open={dialogOpen} onClose={() => setDialogOpen(false)} fullWidth maxWidth="sm"><DialogTitle>Create Fee</DialogTitle><DialogContent sx={{ display: "grid", gap: 2, pt: "16px !important" }}><EntityAutocomplete label="Student" value={formStudent} onChange={(value) => setFormStudent(value)} loadOptions={loadStudents} getOptionKey={(item) => item.studentId} getOptionLabel={studentLabel} /><TextField label="Total amount" type="number" value={totalAmount} onChange={(event) => setTotalAmount(event.target.value)} inputProps={{ min: 0, step: "0.01" }} required /><TextField label="Due date" type="date" value={dueDate} onChange={(event) => setDueDate(event.target.value)} slotProps={{ inputLabel: { shrink: true } }} required /><FormControl><InputLabel>Fee type</InputLabel><Select label="Fee type" value={feeType} onChange={(event) => setFeeType(event.target.value)}>{feeTypes.map((item) => <MenuItem key={item} value={item}>{item}</MenuItem>)}</Select></FormControl><TextField label="Academic year" value={academicYear} onChange={(event) => setAcademicYear(event.target.value)} placeholder="2026-27" required /></DialogContent><DialogActions><Button onClick={() => setDialogOpen(false)}>Cancel</Button><Button variant="contained" onClick={() => void createFee()}>Create</Button></DialogActions></Dialog>
    <Dialog open={!!details} onClose={() => setDetails(null)} fullWidth maxWidth="sm"><DialogTitle>Fee Details</DialogTitle><DialogContent>{details && <Box sx={{ display: "grid", gridTemplateColumns: "auto 1fr", gap: 1.5 }}><Typography color="text.secondary">Fee Type</Typography><Typography>{details.feeType}</Typography><Typography color="text.secondary">Academic Year</Typography><Typography>{details.academicYear}</Typography><Typography color="text.secondary">Total</Typography><Typography>{money(details.totalAmount)}</Typography><Typography color="text.secondary">Paid</Typography><Typography>{money(details.paidAmount)}</Typography><Typography color="text.secondary">Pending</Typography><Typography>{money(details.pendingAmount)}</Typography><Typography color="text.secondary">Due Date</Typography><Typography>{date(details.dueDate)}</Typography><Typography color="text.secondary">Status</Typography><Box><StatusChip status={details.status} /></Box></Box>}</DialogContent><DialogActions><Button onClick={() => setDetails(null)}>Close</Button></DialogActions></Dialog>
  </Box>;
}
