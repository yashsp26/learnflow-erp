import { useState } from "react";

import { useDispatch } from "react-redux";

import { logoutApi } from "../../api/authApi";
import { logout } from "../../features/auth/authSlice";

import { Box, IconButton, Typography, Avatar, Button } from "@mui/material";

import { useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";

import CloseOutlinedIcon from "@mui/icons-material/CloseOutlined";
import MenuOutlinedIcon from "@mui/icons-material/MenuOutlined";
import LogoutOutlinedIcon from "@mui/icons-material/LogoutOutlined";

import HomeOutlinedIcon from "@mui/icons-material/HomeOutlined";

import PersonOutlineOutlinedIcon from "@mui/icons-material/PersonOutlineOutlined";
import SchoolOutlinedIcon from "@mui/icons-material/SchoolOutlined";
import BadgeOutlinedIcon from "@mui/icons-material/BadgeOutlined";

import MenuBookOutlinedIcon from "@mui/icons-material/MenuBookOutlined";
import GroupsOutlinedIcon from "@mui/icons-material/GroupsOutlined";
import FactCheckOutlinedIcon from "@mui/icons-material/FactCheckOutlined";

import PaymentsOutlinedIcon from "@mui/icons-material/PaymentsOutlined";
import ReceiptLongOutlinedIcon from "@mui/icons-material/ReceiptLongOutlined";
import AccountBalanceWalletOutlinedIcon from "@mui/icons-material/AccountBalanceWalletOutlined";
import CurrencyRupeeOutlinedIcon from "@mui/icons-material/CurrencyRupeeOutlined";
import AssessmentOutlinedIcon from "@mui/icons-material/AssessmentOutlined";
import NotificationsActiveOutlinedIcon from "@mui/icons-material/NotificationsActiveOutlined";

import SecurityOutlinedIcon from "@mui/icons-material/SecurityOutlined";
import SettingsOutlinedIcon from "@mui/icons-material/SettingsOutlined";
import ApartmentOutlinedIcon from "@mui/icons-material/ApartmentOutlined";

import SidebarMenuItem from "./SidebarMenuItem";
import SidebarSection from "./SidebarSection";

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

  const dispatch = useDispatch();

  const location = useLocation();

  const [academicsOpen, setAcademicsOpen] = useState(
    location.pathname.startsWith("/courses") ||
      location.pathname.startsWith("/student-courses") ||
      location.pathname.startsWith("/teacher-courses") ||
      location.pathname.startsWith("/student-attendance") ||
      location.pathname.startsWith("/employee-attendance"),
  );

  const [financeOpen, setFinanceOpen] = useState(
    location.pathname.startsWith("/fees") ||
      location.pathname.startsWith("/payments") ||
      location.pathname.startsWith("/scholarships") ||
      location.pathname.startsWith("/refunds") ||
      location.pathname.startsWith("/statements") ||
      location.pathname.startsWith("/reports") ||
      location.pathname.startsWith("/reminders"),
  );

  const [adminOpen, setAdminOpen] = useState(
    location.pathname.startsWith("/designations") ||
      location.pathname.startsWith("/permissions") ||
      location.pathname.startsWith("/notifications"),
  );

  const [settingsOpen, setSettingsOpen] = useState(
    location.pathname.startsWith("/profile") ||
      location.pathname.startsWith("/tenant-settings"),
  );

const handleLogout = async () => {
  try {
    const refreshToken =
      localStorage.getItem("refreshToken");

    if (refreshToken) {
      await logoutApi(refreshToken);
    }
  } catch (error) {
    console.error("Logout failed", error);
  }

  dispatch(logout());

  navigate("/");
};

  return (
    <Box
      sx={{
        width: collapsed ? 90 : 260,

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
        }}
      >
        {!collapsed && (
          <Typography
            variant="h5"
            sx={{
              fontWeight: 800,
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
        >
          {collapsed ? <MenuOutlinedIcon /> : <CloseOutlinedIcon />}
        </IconButton>
      </Box>

      {/* MENU */}

      <Box
        sx={{
          flexGrow: 1,
          overflowY: "auto",
          padding: 2,
        }}
      >
        <SidebarMenuItem
          title="Dashboard"
          icon={<HomeOutlinedIcon />}
          path="/dashboard"
          collapsed={collapsed}
          isMobile={isMobile}
          closeMobileSidebar={closeMobileSidebar}
        />

        <SidebarMenuItem
          title="Users"
          icon={<PersonOutlineOutlinedIcon />}
          path="/users"
          collapsed={collapsed}
        />

        <SidebarMenuItem
          title="Employees"
          icon={<BadgeOutlinedIcon />}
          path="/employees"
          collapsed={collapsed}
        />

        <SidebarMenuItem
          title="Students"
          icon={<SchoolOutlinedIcon />}
          path="/students"
          collapsed={collapsed}
        />

        {!collapsed && (
          <SidebarSection
            title="ACADEMICS"
            open={academicsOpen}
            setOpen={setAcademicsOpen}
          >
            <SidebarMenuItem
              title="Courses"
              icon={<MenuBookOutlinedIcon />}
              path="/courses"
              collapsed={collapsed}
            />

            <SidebarMenuItem
              title="Student Courses"
              icon={<GroupsOutlinedIcon />}
              path="/student-courses"
              collapsed={collapsed}
            />

            <SidebarMenuItem
              title="Teacher Courses"
              icon={<GroupsOutlinedIcon />}
              path="/teacher-courses"
              collapsed={collapsed}
            />

            <SidebarMenuItem
              title="Student Attendance"
              icon={<FactCheckOutlinedIcon />}
              path="/student-attendance"
              collapsed={collapsed}
            />

            <SidebarMenuItem
              title="Employee Attendance"
              icon={<FactCheckOutlinedIcon />}
              path="/employee-attendance"
              collapsed={collapsed}
            />
          </SidebarSection>
        )}

        {!collapsed && (
          <SidebarSection
            title="FINANCE"
            open={financeOpen}
            setOpen={setFinanceOpen}
          >
            <SidebarMenuItem
              title="Fees"
              icon={<PaymentsOutlinedIcon />}
              path="/fees"
              collapsed={collapsed}
            />

            <SidebarMenuItem
              title="Payments"
              icon={<CurrencyRupeeOutlinedIcon />}
              path="/payments"
              collapsed={collapsed}
            />

            <SidebarMenuItem
              title="Scholarships"
              icon={<AccountBalanceWalletOutlinedIcon />}
              path="/scholarships"
              collapsed={collapsed}
            />

            <SidebarMenuItem
              title="Refunds"
              icon={<ReceiptLongOutlinedIcon />}
              path="/refunds"
              collapsed={collapsed}
            />

            <SidebarMenuItem
              title="Statements"
              icon={<AssessmentOutlinedIcon />}
              path="/statements"
              collapsed={collapsed}
            />

            <SidebarMenuItem
              title="Reports"
              icon={<AssessmentOutlinedIcon />}
              path="/reports"
              collapsed={collapsed}
            />

            <SidebarMenuItem
              title="Reminders"
              icon={<NotificationsActiveOutlinedIcon />}
              path="/reminders"
              collapsed={collapsed}
            />
          </SidebarSection>
        )}

        {!collapsed && (
          <SidebarSection
            title="ADMINISTRATION"
            open={adminOpen}
            setOpen={setAdminOpen}
          >
            <SidebarMenuItem
              title="Designations"
              icon={<BadgeOutlinedIcon />}
              path="/designations"
              collapsed={collapsed}
            />

            <SidebarMenuItem
              title="Permissions"
              icon={<SecurityOutlinedIcon />}
              path="/permissions"
              collapsed={collapsed}
            />

            <SidebarMenuItem
              title="Notifications"
              icon={<NotificationsActiveOutlinedIcon />}
              path="/notifications"
              collapsed={collapsed}
            />
          </SidebarSection>
        )}

        {!collapsed && (
          <SidebarSection
            title="SETTINGS"
            open={settingsOpen}
            setOpen={setSettingsOpen}
          >
            <SidebarMenuItem
              title="Profile"
              icon={<SettingsOutlinedIcon />}
              path="/profile"
              collapsed={collapsed}
            />

            <SidebarMenuItem
              title="Tenant Settings"
              icon={<ApartmentOutlinedIcon />}
              path="/tenant-settings"
              collapsed={collapsed}
            />
          </SidebarSection>
        )}
      </Box>

      {/* FOOTER */}

      <Box
        sx={{
          margin: 2,
          marginBottom: 3,
        }}
      >
        {!collapsed && (
          <>
            <Box
              sx={{
                display: "flex",
                alignItems: "center",
                gap: 2,
                mb: 2,
              }}
            >
              <Avatar
                sx={{
                  backgroundColor: "#e86f00",
                }}
              >
                A
              </Avatar>

              <Box>
                <Typography
                  sx={{
                    fontWeight: 600,
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
            </Box>

            <Button
              fullWidth
              variant="outlined"
              startIcon={<LogoutOutlinedIcon />}
              onClick={handleLogout}
            >
              Logout
            </Button>
          </>
        )}
      </Box>
    </Box>
  );
}
