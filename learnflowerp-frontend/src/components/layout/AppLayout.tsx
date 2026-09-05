import { Box, Drawer, IconButton, useMediaQuery } from "@mui/material";

import MenuIcon from "@mui/icons-material/Menu";

import { Outlet } from "react-router-dom";
import { useState } from "react";

import { useEffect } from "react";

import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { fetchMyPermissions } from "../../features/permissions/permissionsSlice";

import Sidebar from "./Sidebar";

export default function AppLayout() {
  const dispatch = useAppDispatch();
  const isAuthenticated = useAppSelector(
    (state) => state.auth.isAuthenticated
  );
  const [collapsed, setCollapsed] = useState(false);

  const [mobileOpen, setMobileOpen] = useState(false);

  const isMobile = useMediaQuery("(max-width:900px)");

  const sidebarWidth = collapsed ? 90 : 250;

  useEffect(() => {
    if (isAuthenticated) {
      void dispatch(fetchMyPermissions());
    }
  }, [dispatch, isAuthenticated]);

  return (
    <Box
      sx={{
        backgroundColor: "background.default",
        minHeight: "100vh",
        padding: {
          xs: 1,
          md: 2,
        },
      }}
    >
      <Box
        sx={{
          display: "flex",
          backgroundColor: "background.paper",
          borderRadius: {
            xs: 1,
            md: 1.5,
          },

          height: {
            xs: "calc(100vh - 8px)",
            md: "calc(100vh - 16px)",
          },

          border: "1px solid",
          borderColor: "divider",
          overflow: "hidden",
        }}
      >
        {/* DESKTOP SIDEBAR */}
        {!isMobile && (
          <Sidebar collapsed={collapsed} setCollapsed={setCollapsed} />
        )}

        {/* MOBILE DRAWER */}
        {isMobile && (
          <Drawer
            open={mobileOpen}
            onClose={() => setMobileOpen(false)}
            slotProps={{
              paper: {
                sx: {
                  borderRadius: "0 12px 12px 0",
                },
              },
            }}
          >
            <Sidebar
              collapsed={false}
              setCollapsed={() => {}}
              isMobile={true}
              closeMobileSidebar={() => setMobileOpen(false)}
            />
          </Drawer>
        )}

        {/* MAIN CONTENT */}
        <Box
          sx={{
            flexGrow: 1,

            width: isMobile ? "100%" : `calc(100% - ${sidebarWidth}px)`,

              p: {
              xs: 2,
              md: 5,
            },

            transition: "0.3s",

            position: "relative",

            overflowY: "auto",
            overflowX: "hidden",

            height: "100%",

            scrollbarWidth: "thin",

            "&::-webkit-scrollbar": {
              width: "6px",
            },

            "&::-webkit-scrollbar-thumb": {
              backgroundColor: "#CBD5E1",
              borderRadius: 1,
            },
          }}
        >
          {/* MOBILE TOP BAR */}
          {isMobile && (
            <Box
              sx={{
                display: "flex",
                alignItems: "center",
                marginBottom: 3,
              }}
            >
              <IconButton
                onClick={() => setMobileOpen(true)}
                sx={{
                  backgroundColor: "background.paper",
                  border: "1px solid",
                  borderColor: "divider",

                  "&:hover": {
                    backgroundColor: "primary.light",
                  },
                }}
              >
                <MenuIcon />
              </IconButton>
            </Box>
          )}

          <Outlet />
        </Box>
      </Box>
    </Box>
  );
}
