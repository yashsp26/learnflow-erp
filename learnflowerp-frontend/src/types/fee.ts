export const feeTypes = ["Tuition", "Exam", "Transport", "Library", "Hostel"] as const;
export const feeStatuses = ["Pending", "Partial", "Paid", "Overdue"] as const;
export type FeeStatus = (typeof feeStatuses)[number];

export interface Fee {
  feeId: number;
  studentId: number;
  totalAmount: number;
  paidAmount: number;
  pendingAmount: number;
  feeType: string;
  academicYear: string;
  status: FeeStatus;
  dueDate: string | null;
}
