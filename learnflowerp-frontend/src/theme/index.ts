import { createTheme, type Shadows } from "@mui/material/styles";

const navy = "#1E3A5F";
const slate = "#2C4A6E";
const border = "#E2E8F0";

export const appTheme = createTheme({
  palette: {
    primary: {
      light: "#E8F0F8",
      main: navy,
      dark: "#142B47",
      contrastText: "#FFFFFF",
    },
    secondary: {
      light: "#E7F6F4",
      main: "#0F766E",
      dark: "#115E59",
      contrastText: "#FFFFFF",
    },
    success: { light: "#DCFCE7", main: "#16A34A", dark: "#15803D" },
    warning: { light: "#FEF3C7", main: "#D97706", dark: "#B45309" },
    error: { light: "#FEE2E2", main: "#DC2626", dark: "#B91C1C" },
    info: { light: "#E0F2FE", main: "#0284C7", dark: "#0369A1" },
    background: { default: "#F6F8FB", paper: "#FFFFFF" },
    divider: border,
    text: { primary: "#172033", secondary: "#667085", disabled: "#98A2B3" },
  },
  typography: {
    fontFamily: "'Inter', sans-serif",
    h1: { fontSize: "2rem", lineHeight: 1.2, fontWeight: 750 },
    h2: { fontSize: "1.75rem", lineHeight: 1.25, fontWeight: 750 },
    h3: { fontSize: "1.5rem", lineHeight: 1.3, fontWeight: 700 },
    h4: { fontSize: "1.375rem", lineHeight: 1.35, fontWeight: 700 },
    h5: { fontSize: "1.125rem", lineHeight: 1.4, fontWeight: 700 },
    h6: { fontSize: "1rem", lineHeight: 1.45, fontWeight: 650 },
    subtitle1: { fontSize: "0.9375rem", lineHeight: 1.5, fontWeight: 600 },
    subtitle2: { fontSize: "0.8125rem", lineHeight: 1.45, fontWeight: 600 },
    body1: { fontSize: "0.9375rem", lineHeight: 1.55 },
    body2: { fontSize: "0.8125rem", lineHeight: 1.5 },
    caption: { fontSize: "0.75rem", lineHeight: 1.4, fontWeight: 500 },
    button: {
      fontSize: "0.875rem",
      lineHeight: 1.25,
      fontWeight: 650,
      textTransform: "none",
    },
  },
  shape: { borderRadius: 10 },
  shadows: [
    "none",
    ...Array(24).fill("0 4px 16px rgba(16, 24, 40, 0.06)"),
  ] as Shadows,
  components: {
    MuiCssBaseline: {
      styleOverrides: {
        body: { backgroundColor: "#F6F8FB", margin: 0 },
        "*": { boxSizing: "border-box" },
        "*::-webkit-scrollbar": { width: 6, height: 6 },
        "*::-webkit-scrollbar-thumb": {
          backgroundColor: "#CBD5E1",
          borderRadius: 8,
        },
        ".MuiDataGrid-root": { border: 0, fontSize: "0.8125rem" },
        ".MuiDataGrid-columnHeaders": {
          backgroundColor: "#F8FAFC",
          borderBottom: `1px solid ${border}`,
          fontWeight: 700,
        },
        ".MuiDataGrid-columnHeaderTitle": { fontWeight: 700, color: "#475467" },
        ".MuiDataGrid-cell": { borderColor: "#EEF2F6" },
        ".MuiDataGrid-row:hover": { backgroundColor: "#F8FAFC" },
        ".MuiDataGrid-footerContainer": { borderTop: `1px solid ${border}` },
      },
    },
    MuiButton: {
      styleOverrides: {
        root: {
          minHeight: 40,
          borderRadius: 8,
          padding: "9px 16px",
          boxShadow: "none",
        },
        contained: {
          "&:hover": { boxShadow: "0 3px 8px rgba(30, 58, 95, 0.18)" },
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          border: `1px solid ${border}`,
          boxShadow: "0 2px 12px rgba(16, 24, 40, 0.05)",
        },
      },
    },
    MuiPaper: { styleOverrides: { rounded: { borderRadius: 10 } } },
    MuiOutlinedInput: {
      styleOverrides: {
        root: {
          backgroundColor: "#FFFFFF",
          borderRadius: 8,
          "& fieldset": { borderColor: "#D0D5DD" },
          "&:hover fieldset": { borderColor: slate },
          "&.Mui-focused fieldset": { borderColor: navy, borderWidth: 1 },
        },
      },
    },
    MuiInputLabel: {
      styleOverrides: { root: { fontSize: "0.875rem", color: "#667085" } },
    },
    MuiDialog: {
      styleOverrides: {
        paper: {
          borderRadius: 12,
          boxShadow: "0 16px 40px rgba(16, 24, 40, 0.16)",
        },
      },
    },
    MuiDialogTitle: {
      styleOverrides: {
        root: {
          padding: "20px 24px",
          fontSize: "1.125rem",
          fontWeight: 700,
          borderBottom: `1px solid ${border}`,
        },
      },
    },
    MuiDialogContent: {
      styleOverrides: { root: { padding: "24px !important" } },
    },
    MuiDialogActions: {
      styleOverrides: {
        root: {
          padding: "16px 24px",
          borderTop: `1px solid ${border}`,
          gap: 8,
        },
      },
    },
    MuiChip: {
      styleOverrides: {
        root: {
          height: 26,
          borderRadius: 6,
          fontWeight: 650,
          fontSize: "0.75rem",
        },
      },
    },
    MuiTabs: { styleOverrides: { indicator: { height: 3, borderRadius: 3 } } },
  },
});
