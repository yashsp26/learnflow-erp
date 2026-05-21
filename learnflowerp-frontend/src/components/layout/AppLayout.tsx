import { Box, Drawer, IconButton, useMediaQuery } from "@mui/material";

import MenuIcon from "@mui/icons-material/Menu";

import { Outlet } from "react-router-dom";
import { useState } from "react";

import Sidebar from "./Sidebar";

export default function AppLayout() {
  const [collapsed, setCollapsed] = useState(false);

  const [mobileOpen, setMobileOpen] = useState(false);

  const isMobile = useMediaQuery("(max-width:900px)");

  const sidebarWidth = collapsed ? 90 : 250;

  return (
    <Box
      sx={{
        backgroundColor: "#f5f3ee",
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
          backgroundColor: "#fcfbf8",
          borderRadius: {
            xs: "16px",
            md: "28px",
          },

          height: {
            xs: "calc(100vh - 8px)",
            md: "calc(100vh - 16px)",
          },

          border: "1px solid #ece7df",
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
                  borderRadius: "0 20px 20px 0",
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

            padding: {
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
              backgroundColor: "#d1d5db",
              borderRadius: "10px",
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
                  backgroundColor: "#fff",
                  border: "1px solid #ece7df",

                  "&:hover": {
                    backgroundColor: "#fff1e6",
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
