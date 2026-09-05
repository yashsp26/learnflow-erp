import { Dialog, DialogContent, DialogTitle, Divider, Stack, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import toast from "react-hot-toast";
import { getEmployeeByIdApi, updateEmployeeDocumentApi } from "../../api/employeeApi";
import FileUpload from "../../components/common/FileUpload";
import RequirePermission from "../../components/common/RequirePermission";
import type { Employee } from "../../types/employee";

type Props = { open: boolean; employeeId: number | null; designationName: string; onClose: () => void };
const value = (item: string | number | null) => item ?? "—";

export default function EmployeeDetailsDialog({ open, employeeId, designationName, onClose }: Props) {
  const [employee, setEmployee] = useState<Employee | null>(null);
  const [uploadUrl, setUploadUrl] = useState<string | null>(null);

  useEffect(() => {
    const load = async () => {
      if (!employeeId) return;
      try {
        const response = await getEmployeeByIdApi(employeeId);
        setEmployee(response.data ?? response);
      } catch { toast.error("Failed to load employee"); }
    };
    if (open) void load();
  }, [open, employeeId]);

  useEffect(() => {
    const save = async () => {
      if (!employeeId || !uploadUrl) return;
      try {
        await updateEmployeeDocumentApi(employeeId, uploadUrl);
        setEmployee((current) => current ? { ...current, documentUrl: uploadUrl } : current);
        toast.success("Document updated successfully");
      } catch { toast.error("Failed to update document"); }
    };
    void save();
  }, [employeeId, uploadUrl]);

  return <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
    <DialogTitle>Employee Details</DialogTitle>
    <DialogContent>{employee && <Stack spacing={2}><Divider />
      <Typography><strong>Employee code:</strong> {employee.empCode}</Typography>
      <Typography><strong>Name:</strong> {value(employee.firstName)} {value(employee.lastName)}</Typography>
      <Typography><strong>Email:</strong> {value(employee.email)}</Typography>
      <Typography><strong>Department:</strong> {value(employee.department)}</Typography>
      <Typography><strong>Designation:</strong> {designationName}</Typography>
      <Typography><strong>Salary:</strong> {employee.salary == null ? "—" : employee.salary.toLocaleString("en-IN")}</Typography>
      <RequirePermission name="UpdateEmployee"><FileUpload value={employee.documentUrl} onUploaded={setUploadUrl} /></RequirePermission>
    </Stack>}</DialogContent>
  </Dialog>;
}
