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
        gap: 2,
        padding: "12px 14px",
        borderRadius: "14px",
        cursor: "pointer",

        backgroundColor: active
          ? "#fff1e6"
          : "transparent",

        color: active
          ? "#e86f00"
          : "#7c4a21",

        "&:hover": {
          backgroundColor: "#fff1e6",
          color: "#e86f00",
        },
      }}
    >
      {icon}

      {!collapsed && (
        <Typography
          sx={{
            fontWeight: active
              ? 700
              : 600,
            fontSize: "15px",
          }}
        >
          {title}
        </Typography>
      )}
    </Box>
  );
}