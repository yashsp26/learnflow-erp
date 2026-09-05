import { Chip } from "@mui/material";

type Props = {
  active?: boolean;
  status?: "Present" | "Absent" | "Leave" | "HalfDay" | "Pending" | "Partial" | "Paid" | "Overdue" | "Success" | "Failed" | "Refunded" | "Cancelled";
};

export default function StatusChip({
  active,
  status,
}: Props) {
  const label = status ?? (active ? "Active" : "Inactive");
  const color = status === "Present" || status === "Paid" || status === "Success" || (!status && active)
    ? "success"
    : status === "Absent" || status === "Failed" || status === "Overdue"
      ? "error"
      : status === "HalfDay" || status === "Partial" || status === "Pending"
        ? "warning"
        : status === "Leave" || status === "Refunded" || status === "Cancelled"
          ? "info"
          : "default";
  return (
    <Chip
      label={label}
      color={color}
      size="small"
    />
  );
}
