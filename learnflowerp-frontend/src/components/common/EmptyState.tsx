import { Box, Typography } from "@mui/material";
import InboxOutlinedIcon from "@mui/icons-material/InboxOutlined";

type Props = {
  title: string;
  subtitle?: string;
};

export default function EmptyState({ title, subtitle }: Props) {
  return (
    <Box
      sx={{
        textAlign: "center",
        py: 7,
        px: 2,
      }}
    >
      <Box sx={{ width: 48, height: 48, display: "grid", placeItems: "center", mx: "auto", mb: 2, borderRadius: "50%", bgcolor: "primary.light", color: "primary.main" }}>
        <InboxOutlinedIcon />
      </Box>
      <Typography variant="h6">
        {title}
      </Typography>

      {subtitle && <Typography variant="body2" color="text.secondary" sx={{ mt: 0.75 }}>{subtitle}</Typography>}
    </Box>
  );
}
