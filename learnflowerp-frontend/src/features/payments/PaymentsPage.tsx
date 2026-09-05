import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControl, InputLabel, MenuItem, Select, Tab, Tabs, TextField, Typography } from "@mui/material";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { useCallback, useState } from "react";
import toast from "react-hot-toast";
import { getStudentFeesApi } from "../../api/feeApi";
import { getStudentsApi } from "../../api/studentApi";
import { createPaymentApi, downloadReceiptPdfApi, getPaymentByIdApi, getReceiptApi, getStudentPaymentsApi } from "../../api/paymentApi";
import EntityAutocomplete from "../../components/common/EntityAutocomplete";
import EmptyState from "../../components/common/EmptyState";
import PageCard from "../../components/common/PageCard";
import PageHeader from "../../components/common/PageHeader";
import RequirePermission from "../../components/common/RequirePermission";
import StatusChip from "../../components/common/StatusChip";
import type { Fee } from "../../types/fee";
import type { Payment, PaymentMethod, Receipt } from "../../types/payment";
import { paymentMethods } from "../../types/payment";
import type { Student } from "../../types/student";
import { useHasPermission } from "../permissions/useHasPermission";

const unwrap = <T,>(response: { data?: T } | T): T => (response as { data?: T }).data ?? response as T;
const money = (amount: number) => new Intl.NumberFormat("en-IN", { style: "currency", currency: "INR" }).format(amount);
const date = (value: string) => value ? new Date(value).toLocaleDateString() : "—";
const studentLabel = (student: Student) => `${[student.firstName, student.lastName].filter(Boolean).join(" ") || student.email} (${student.enrollmentNo})`;

export default function PaymentsPage() {
  const canCreate = useHasPermission("CreatePayment");
  const canView = useHasPermission("ViewPayment");
  const tabs = [canCreate && "create", canView && "student"].filter(Boolean) as string[];
  const [tab, setTab] = useState("create");
  const activeTab = tabs.includes(tab) ? tab : (tabs[0] ?? "none");
  const [student, setStudent] = useState<Student | null>(null);
  const [fees, setFees] = useState<Fee[]>([]);
  const [fee, setFee] = useState<Fee | null>(null);
  const [amount, setAmount] = useState("");
  const [method, setMethod] = useState<PaymentMethod>("Cash");
  const [transactionRef, setTransactionRef] = useState("");
  const [remarks, setRemarks] = useState("");
  const [listStudent, setListStudent] = useState<Student | null>(null);
  const [payments, setPayments] = useState<Payment[]>([]);
  const [loading, setLoading] = useState(false);
  const [details, setDetails] = useState<Payment | null>(null);
  const [receipt, setReceipt] = useState<Receipt | null>(null);
  const loadStudents = useCallback(async (search: string) => { const page = unwrap(await getStudentsApi(1, 100, search)) as { items?: Student[] }; return page.items ?? []; }, []);
  const loadOutstandingFees = useCallback(async (search: string) => fees.filter((item) => `${item.feeType} ${item.academicYear}`.toLowerCase().includes(search.toLowerCase())), [fees]);
  const selectPaymentStudent = async (value: Student | null) => { setStudent(value); setFees([]); setFee(null); setAmount(""); if (!value) return; try { setLoading(true); setFees((unwrap(await getStudentFeesApi(value.studentId)) as Fee[]).filter((item) => item.pendingAmount > 0)); } catch { toast.error("Failed to load outstanding fees"); } finally { setLoading(false); } };
  const selectFee = (value: Fee | null) => { setFee(value); setAmount(value ? String(value.pendingAmount) : ""); };
  const createPayment = async () => { const amountPaid = Number(amount); if (!fee || !amountPaid || amountPaid <= 0) { toast.error("Enter a valid payment amount"); return; } if (amountPaid > fee.pendingAmount) { toast.error("Payment amount cannot exceed the pending amount"); return; } try { await createPaymentApi(fee.feeId, amountPaid, method, transactionRef, remarks); toast.success("Payment created successfully"); setFee(null); setFees((items) => items.filter((item) => item.feeId !== fee.feeId)); setAmount(""); setTransactionRef(""); setRemarks(""); } catch { toast.error("Failed to create payment"); } };
  const selectListStudent = async (value: Student | null) => { setListStudent(value); setPayments([]); if (!value) return; try { setLoading(true); setPayments(unwrap(await getStudentPaymentsApi(value.studentId)) as Payment[]); } catch { toast.error("Failed to load student payments"); } finally { setLoading(false); } };
  const showDetails = async (paymentId: number) => { try { setDetails(unwrap(await getPaymentByIdApi(paymentId)) as Payment); } catch { toast.error("Failed to load payment details"); } };
  const showReceipt = async (paymentId: number) => { try { setReceipt(unwrap(await getReceiptApi(paymentId)) as Receipt); } catch { toast.error("Failed to load receipt"); } };
  const downloadPdf = async (payment: Payment) => { try { const blob = await downloadReceiptPdfApi(payment.paymentId); const url = URL.createObjectURL(blob); const anchor = document.createElement("a"); anchor.href = url; anchor.download = `Receipt-${payment.receiptNumber}.pdf`; document.body.appendChild(anchor); anchor.click(); anchor.remove(); URL.revokeObjectURL(url); } catch { toast.error("Failed to download receipt PDF"); } };
  const columns: GridColDef<Payment>[] = [{ field: "amountPaid", headerName: "Amount", flex: 1, valueGetter: (_v, row) => money(row.amountPaid) }, { field: "paymentMethod", headerName: "Method", flex: 1 }, { field: "status", headerName: "Status", width: 125, renderCell: (params) => <StatusChip status={params.row.status} /> }, { field: "receiptNumber", headerName: "Receipt No.", flex: 1.2 }, { field: "paymentDate", headerName: "Date", flex: 1, valueGetter: (_v, row) => date(row.paymentDate) }];
  if (!tabs.length) return <Box><PageHeader title="Payments" subtitle="Record payments and issue receipts" /><PageCard><EmptyState title="No access" subtitle="You do not have permission to view or create payments." /></PageCard></Box>;
  return <Box><PageHeader title="Payments" subtitle="Record payments and issue receipts" /><Tabs value={activeTab} onChange={(_event, value) => setTab(value)} sx={{ mb: 3 }}>{canCreate && <Tab value="create" label="Create Payment" />}{canView && <Tab value="student" label="By Student" />}</Tabs>
    {activeTab === "create" && <RequirePermission name="CreatePayment"><PageCard><Box sx={{ display: "grid", gridTemplateColumns: { xs: "1fr", md: "1fr 1fr" }, gap: 2 }}><EntityAutocomplete label="Student" value={student} onChange={(value) => void selectPaymentStudent(value)} loadOptions={loadStudents} getOptionKey={(item) => item.studentId} getOptionLabel={studentLabel} /><EntityAutocomplete label="Outstanding fee" value={fee} onChange={selectFee} loadOptions={loadOutstandingFees} getOptionKey={(item) => item.feeId} getOptionLabel={(item) => `${item.feeType} (${item.academicYear}) — ${money(item.pendingAmount)} pending`} disabled={!student} /><TextField label="Amount paid" type="number" value={amount} onChange={(event) => setAmount(event.target.value)} inputProps={{ min: 0, max: fee?.pendingAmount, step: "0.01" }} disabled={!fee} helperText={fee ? `Maximum ${money(fee.pendingAmount)}` : undefined} /><FormControl disabled={!fee}><InputLabel>Payment method</InputLabel><Select label="Payment method" value={method} onChange={(event) => setMethod(event.target.value as PaymentMethod)}>{paymentMethods.map((item) => <MenuItem key={item} value={item}>{item}</MenuItem>)}</Select></FormControl><TextField label="Transaction reference (optional)" value={transactionRef} onChange={(event) => setTransactionRef(event.target.value)} disabled={!fee} /><TextField label="Remarks (optional)" value={remarks} onChange={(event) => setRemarks(event.target.value)} disabled={!fee} /><Box><Button variant="contained" onClick={() => void createPayment()} disabled={!fee || loading}>Create payment</Button></Box></Box></PageCard></RequirePermission>}
    {activeTab === "student" && <><Box sx={{ maxWidth: 560, mb: 3 }}><EntityAutocomplete label="Select student" value={listStudent} onChange={(value) => void selectListStudent(value)} loadOptions={loadStudents} getOptionKey={(item) => item.studentId} getOptionLabel={studentLabel} /></Box><PageCard><Box sx={{ height: 560 }}><DataGrid rows={payments} columns={columns} loading={loading} getRowId={(row) => row.paymentId} onRowClick={(params) => void showDetails(params.row.paymentId)} disableRowSelectionOnClick pageSizeOptions={[10, 25, 50]} /></Box></PageCard></>}
    <Dialog open={!!details} onClose={() => setDetails(null)} fullWidth maxWidth="sm"><DialogTitle>Payment Details</DialogTitle><DialogContent>{details && <Box sx={{ display: "grid", gridTemplateColumns: "auto 1fr", gap: 1.5 }}><Typography color="text.secondary">Receipt</Typography><Typography>{details.receiptNumber}</Typography><Typography color="text.secondary">Amount</Typography><Typography>{money(details.amountPaid)}</Typography><Typography color="text.secondary">Method</Typography><Typography>{details.paymentMethod}</Typography><Typography color="text.secondary">Status</Typography><Box><StatusChip status={details.status} /></Box><Typography color="text.secondary">Transaction Ref.</Typography><Typography>{details.transactionRef || "—"}</Typography><Typography color="text.secondary">Date</Typography><Typography>{date(details.paymentDate)}</Typography></Box>}</DialogContent><DialogActions><RequirePermission name="ViewPayment"><Button onClick={() => details && void showReceipt(details.paymentId)}>View Receipt</Button><Button onClick={() => details && void downloadPdf(details)}>Download PDF</Button></RequirePermission><Button onClick={() => setDetails(null)}>Close</Button></DialogActions></Dialog>
    <Dialog open={!!receipt} onClose={() => setReceipt(null)} fullWidth maxWidth="sm"><DialogTitle>Receipt</DialogTitle><DialogContent>{receipt && <Box sx={{ border: "1px solid", borderColor: "divider", borderRadius: 2, p: 3, display: "grid", gridTemplateColumns: "auto 1fr", gap: 1.5 }}><Typography color="text.secondary">Receipt No.</Typography><Typography fontWeight={700}>{receipt.receiptNumber}</Typography><Typography color="text.secondary">Student</Typography><Typography>{receipt.studentName}</Typography><Typography color="text.secondary">Fee Type</Typography><Typography>{receipt.feeType}</Typography><Typography color="text.secondary">Amount Paid</Typography><Typography>{money(receipt.amountPaid)}</Typography><Typography color="text.secondary">Method</Typography><Typography>{receipt.paymentMethod}</Typography><Typography color="text.secondary">Transaction Ref.</Typography><Typography>{receipt.transactionRef || "—"}</Typography><Typography color="text.secondary">Date</Typography><Typography>{date(receipt.paymentDate)}</Typography></Box>}</DialogContent><DialogActions><Button onClick={() => setReceipt(null)}>Close</Button></DialogActions></Dialog>
  </Box>;
}
