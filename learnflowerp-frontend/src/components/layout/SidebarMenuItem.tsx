import { Box, Typography } from "@mui/material";
import { useLocation, useNavigate } from "react-router-dom";

type Props = {
  title: string;
  icon: React.ReactNode;
  path: string;
  collapsed: boolean;
  isMobile?: boolean;
  closeMobileSidebar?: () => void;
};

export default function SidebarMenuItem({
  title,
  icon,
  path,
  collapsed,
  isMobile,
  closeMobileSidebar,
}: Props) {
  const navigate = useNavigate();
  const location = useLocation();

  const active =
    location.pathname === path;

  return (
    <Box
      onClick={() => {
        navigate(path);

        if (isMobile) {
          closeMobileSidebar?.();
        }
      }}
      sx={{
        display: "flex",
        alignItems: "center",
        gap: 1.5,
        px: 1.5,
        py: 1.25,
        borderRadius: 1,
        cursor: "pointer",
        borderLeft: "3px solid",
        borderLeftColor: active ? "primary.main" : "transparent",
        backgroundColor: active ? "primary.light" : "transparent",
        color: active ? "primary.main" : "text.secondary",

        "&:hover": {
          backgroundColor: "action.hover",
          color: "primary.main",
        },
      }}
    >
      {icon}

      {!collapsed && (
        <Typography
          sx={{
            fontWeight: active ? 700 : 550,
            fontSize: "0.875rem",
          }}
        >
          {title}
        </Typography>
      )}
    </Box>
  );
}
