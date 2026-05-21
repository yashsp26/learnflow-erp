import { Box, IconButton, Typography, Avatar } from "@mui/material";
import CloseOutlinedIcon from "@mui/icons-material/CloseOutlined";
import MenuOutlinedIcon from "@mui/icons-material/MenuOutlined";
import HomeOutlinedIcon from "@mui/icons-material/HomeOutlined";
import Inventory2OutlinedIcon from "@mui/icons-material/Inventory2Outlined";
import PaymentsOutlinedIcon from "@mui/icons-material/PaymentsOutlined";
import TuneOutlinedIcon from "@mui/icons-material/TuneOutlined";
import { useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import LogoutOutlinedIcon from "@mui/icons-material/LogoutOutlined";
import SchoolOutlinedIcon from "@mui/icons-material/SchoolOutlined";

import BadgeOutlinedIcon from "@mui/icons-material/BadgeOutlined";
type Props = {
  collapsed: boolean;
  setCollapsed: (value: boolean) => void;
  isMobile?: boolean;
  closeMobileSidebar?: () => void;
};

export default function Sidebar({
  collapsed,
  setCollapsed,
  isMobile = false,
  closeMobileSidebar,
}: Props) {
  const navigate = useNavigate();
  const location = useLocation();
  const handleLogout = () => {
    localStorage.removeItem("token");

    localStorage.removeItem("refreshToken");

    localStorage.removeItem("userEmail");

    navigate("/");
  };

  const menus = [
    {
      title: "Home",
      icon: <HomeOutlinedIcon />,
      path: "/dashboard",
    },
    {
      title: "Courses",
      icon: <Inventory2OutlinedIcon />,
      path: "/courses",
    },
    {
      title: "Finance",
      icon: <PaymentsOutlinedIcon />,
      path: "/finance",
    },
    
    {
      title: "Students",
      icon: <SchoolOutlinedIcon />,
      path: "/students",
    },

    {
      title: "Employees",
      icon: <BadgeOutlinedIcon />,
      path: "/employees",
    },
    {
      title: "Admin",
      icon: <TuneOutlinedIcon />,
      path: "/users",
    },
  ];

  return (
    <Box
      sx={{
        width: collapsed ? 90 : 250,
        transition: "all 0.3s ease",
        backgroundColor: "#fff",
        borderRight: "1px solid #ece7df",
        display: "flex",
        flexDirection: "column",
        height: "100vh",
        overflow: "hidden",
        flexShrink: 0,
      }}
    >
      {/* HEADER */}
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
          justifyContent: collapsed ? "center" : "space-between",
          padding: 2,
          borderBottom: "1px solid #f3f4f6",
          flexShrink: 0,
        }}
      >
        {!collapsed && (
          <Typography
            variant="h5"
            sx={{
              fontWeight: 800,
              color: "#111827",
            }}
          >
            LearnFlowERP
          </Typography>
        )}

        <IconButton
          onClick={() => {
            if (isMobile) {
              closeMobileSidebar?.();
            } else {
              setCollapsed(!collapsed);
            }
          }}
          sx={{
            backgroundColor: "#f9fafb",

            "&:hover": {
              backgroundColor: "#fff1e6",
              color: "#e86f00",
            },
          }}
        >
          {collapsed ? <MenuOutlinedIcon /> : <CloseOutlinedIcon />}
        </IconButton>
      </Box>

      {/* MENU */}
      <Box
        sx={{
          flexGrow: 1,
          overflowY: "auto",
          overflowX: "hidden",
          padding: 2,

          display: "flex",
          flexDirection: "column",
          gap: 1,

          scrollbarWidth: "thin",

          "&::-webkit-scrollbar": {
            width: "6px",
          },

          "&::-webkit-scrollbar-thumb": {
            backgroundColor: "#d1d5db",
            borderRadius: "10px",
          },
        }}
      >
        {menus.map((menu, index) => {
          const active = location.pathname === menu.path;

          return (
            <Box
              key={index}
              onClick={() => {
                navigate(menu.path);

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
                transition: "0.2s",

                backgroundColor: active ? "#fff1e6" : "transparent",

                color: active ? "#e86f00" : "#7c4a21",

                "&:hover": {
                  backgroundColor: "#fff1e6",
                  color: "#e86f00",
                },
              }}
            >
              <Box>{menu.icon}</Box>

              {!collapsed && (
                <Typography
                  sx={{
                    fontWeight: active ? 700 : 600,
                    fontSize: "15px",
                  }}
                >
                  {menu.title}
                </Typography>
              )}
            </Box>
          );
        })}
      </Box>

      {/* FOOTER */}
      <Box
        sx={{
          padding: 2,
          borderTop: "1px solid #f3f4f6",
          flexShrink: 0,
          margin: "15px",
        }}
      >
        <Box
          sx={{
            display: "flex",
            alignItems: "center",
            justifyContent: collapsed ? "center" : "space-between",
            gap: 2,
          }}
        >
          <Box
            sx={{
              display: "flex",
              alignItems: "center",
              gap: 2,
            }}
          >
            <Avatar
              sx={{
                backgroundColor: "#e86f00",
              }}
            >
              A
            </Avatar>

            {!collapsed && (
              <Box>
                <Typography
                  sx={{
                    fontWeight: 600,
                    fontSize: "14px",
                  }}
                >
                  Admin
                </Typography>

                <Typography
                  sx={{
                    fontSize: "12px",
                    color: "#6b7280",
                  }}
                >
                  {localStorage.getItem("userEmail") || "admin@learnflow.com"}
                </Typography>
              </Box>
            )}
          </Box>

          {!collapsed && (
            <IconButton
              onClick={handleLogout}
              sx={{
                backgroundColor: "#f9fafb",

                "&:hover": {
                  backgroundColor: "#fff1e6",
                  color: "#e86f00",
                },
              }}
            >
              <LogoutOutlinedIcon />
            </IconButton>
          )}
        </Box>
      </Box>
    </Box>
  );
}
