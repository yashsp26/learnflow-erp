import {
  Typography,
  Paper,
} from "@mui/material";

export default function CoursesPage() {
  return (
    <Paper sx={{ padding: 3 }}>
      <Typography variant="h4">
        Courses
      </Typography>

      <Typography sx={{ mt: 2 }}>
        Courses management page.
      </Typography>
    </Paper>
  );
}