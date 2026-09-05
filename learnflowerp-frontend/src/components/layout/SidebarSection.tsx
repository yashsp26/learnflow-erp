import {
  Box,
  Typography,
  IconButton,
} from "@mui/material";

import ExpandLessIcon from "@mui/icons-material/ExpandLess";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";

type Props = {
  title: string;
  open: boolean;
  setOpen: (
    value: boolean
  ) => void;
  children: React.ReactNode;
};

export default function SidebarSection({
  title,
  open,
  setOpen,
  children,
}: Props) {
  return (
    <Box
      sx={{
        mt: 2.5,
      }}
    >
      <Box
        onClick={() =>
          setOpen(!open)
        }
        sx={{
          display: "flex",
          alignItems: "center",
          justifyContent:
            "space-between",
          cursor: "pointer",
          px: 1,
        }}
      >
        <Typography
          sx={{
            fontSize: "0.6875rem",
            color: "text.disabled",
            fontWeight: 700,
          }}
        >
          {title}
        </Typography>

        <IconButton size="small">
          {open ? (
            <ExpandLessIcon />
          ) : (
            <ExpandMoreIcon />
          )}
        </IconButton>
      </Box>

      {open && children}
    </Box>
  );
}
