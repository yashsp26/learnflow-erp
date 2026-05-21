import {
  AppBar,
  Box,
  Toolbar,
  Typography,
  Avatar,
} from "@mui/material";

export default function Navbar() {
  return (
    <AppBar
      position="fixed"
      elevation={0}
      sx={{
        backgroundColor: "#f5f3ee",
        color: "#111827",
        borderBottom: "1px solid #e5e7eb",
        zIndex: 1201,
      }}
    >
      <Toolbar>
        <Typography
          variant="h6"
          sx={{
            flexGrow: 1,
            fontWeight: 700,
          }}
        >
          LearnFlowERP
        </Typography>

        <Box
          sx={{
            display: "flex",
            alignItems: "center",
            gap: 2,
          }}
        >
          <Typography
            sx={{
              fontWeight: 500,
            }}
          >
            Admin
          </Typography>

          <Avatar
            sx={{
              backgroundColor: "#f5f3ee",
            }}
          >
            A
          </Avatar>
        </Box>
      </Toolbar>
    </AppBar>
  );
}