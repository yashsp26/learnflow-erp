import { Box, Button, IconButton } from "@mui/material";
import { AddOutlined, DeleteOutlined, VisibilityOutlined } from "@mui/icons-material";
import { DataGrid } from "@mui/x-data-grid";
import type { GridColDef, GridPaginationModel } from "@mui/x-data-grid";
import { useCallback, useEffect, useState } from "react";
import toast from "react-hot-toast";
import { deleteEmployeeApi, getEmployeesApi } from "../../api/employeeApi";
import { getDesignationsApi } from "../../api/designationApi";
import ConfirmDialog from "../../components/common/ConfirmDialog";
import EmptyState from "../../components/common/EmptyState";
import PageCard from "../../components/common/PageCard";
import PageHeader from "../../components/common/PageHeader";
import RequirePermission from "../../components/common/RequirePermission";
import SearchBar from "../../components/common/SearchBar";
import type { Designation } from "../../types/designation";
import type { Employee } from "../../types/employee";
import CreateUserDialog from "../users/CreateUserDialog";
import EmployeeDetailsDialog from "./EmployeeDetailsDialog";

export default function EmployeesPage() {
  const [employees, setEmployees] = useState<Employee[]>([]); const [designations, setDesignations] = useState<Designation[]>([]);
  const [search, setSearch] = useState(""); const [loading, setLoading] = useState(false); const [totalCount, setTotalCount] = useState(0);
  const [model, setModel] = useState<GridPaginationModel>({ page: 0, pageSize: 10 }); const [selected, setSelected] = useState<Employee | null>(null);
  const [createOpen, setCreateOpen] = useState(false); const [deleteId, setDeleteId] = useState<number | null>(null);
  const fetchEmployees = useCallback(async () => { try { setLoading(true); const response = await getEmployeesApi(model.page + 1, model.pageSize, search); const page = response.data ?? response; setEmployees(page.items ?? []); setTotalCount(page.totalCount ?? 0); } catch { toast.error("Failed to load employees"); } finally { setLoading(false); } }, [model, search]);
  useEffect(() => { void fetchEmployees(); }, [fetchEmployees]);
  useEffect(() => { getDesignationsApi().then((response) => setDesignations(response.data ?? [])).catch(() => toast.error("Failed to load designations")); }, []);
  const designation = (id: number) => designations.find((item) => item.designationId === id)?.name ?? "Unassigned";
  const columns: GridColDef<Employee>[] = [
    { field: "empCode", headerName: "Employee Code", flex: 1 }, { field: "firstName", headerName: "Name", flex: 1.2, valueGetter: (_value, row) => [row.firstName, row.lastName].filter(Boolean).join(" ") || "—" },
    { field: "department", headerName: "Department", flex: 1, valueGetter: (_value, row) => row.department ?? "—" }, { field: "designationId", headerName: "Designation", flex: 1, valueGetter: (_value, row) => designation(row.designationId) }, { field: "email", headerName: "Email", flex: 1.5, valueGetter: (_value, row) => row.email ?? "—" },
    { field: "actions", headerName: "Actions", width: 120, sortable: false, renderCell: (params) => <Box><IconButton onClick={() => setSelected(params.row)}><VisibilityOutlined /></IconButton><RequirePermission name="DeleteEmployee"><IconButton color="error" onClick={() => setDeleteId(params.row.employeeId)}><DeleteOutlined /></IconButton></RequirePermission></Box> },
  ];
  return <Box><PageHeader title="Employees" subtitle="Manage all employees" action={<RequirePermission name="CreateUser"><Button variant="contained" startIcon={<AddOutlined />} onClick={() => setCreateOpen(true)}>Add Employee</Button></RequirePermission>} />
    <Box sx={{ mb: 3 }}><SearchBar value={search} onChange={(value) => { setSearch(value); setModel((current) => ({ ...current, page: 0 })); }} placeholder="Search employees..." /></Box>
    <PageCard>{employees.length === 0 && !loading ? <EmptyState title={search ? "No matching employees" : "No employees yet"} /> : <Box sx={{ height: 600 }}><DataGrid rows={employees} columns={columns} loading={loading} getRowId={(row) => row.employeeId} disableRowSelectionOnClick paginationMode="server" rowCount={totalCount} paginationModel={model} onPaginationModelChange={setModel} pageSizeOptions={[10, 20, 50]} /></Box>}</PageCard>
    <EmployeeDetailsDialog open={!!selected} employeeId={selected?.employeeId ?? null} designationName={selected ? designation(selected.designationId) : "—"} onClose={() => setSelected(null)} />
    <CreateUserDialog open={createOpen} onClose={() => setCreateOpen(false)} onSuccess={() => setCreateOpen(false)} defaultRoleId={3} title="Add Employee — create login" onboardingMessage="The employee record appears after this user completes their profile on first login." />
    <ConfirmDialog open={!!deleteId} title="Delete Employee" message="Are you sure you want to delete this employee?" onClose={() => setDeleteId(null)} onConfirm={async () => { if (!deleteId) return; try { await deleteEmployeeApi(deleteId); toast.success("Employee deleted successfully"); setDeleteId(null); await fetchEmployees(); } catch { toast.error("Failed to delete employee"); } }} />
  </Box>;
}
