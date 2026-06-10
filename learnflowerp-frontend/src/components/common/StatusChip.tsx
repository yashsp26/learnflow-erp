import { Chip } from "@mui/material";

type Props = {
  active: boolean;
};

export default function StatusChip({
  active,
}: Props) {
  return (
    <Chip
      label={
        active
          ? "Active"
          : "Inactive"
      }
      color={
        active
          ? "success"
          : "default"
      }
      size="small"
    />
  );
}