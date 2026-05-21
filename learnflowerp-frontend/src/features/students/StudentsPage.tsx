import {
  Typography,
  Paper,
} from "@mui/material";

export default function StudentsPage() {
  return (
    <Paper sx={{ padding: 3 }}>
      <Typography variant="h4">
        Students
      </Typography>

      <Typography sx={{ mt: 2 }}>
        Students management page.
      </Typography>
    </Paper>
  );
}