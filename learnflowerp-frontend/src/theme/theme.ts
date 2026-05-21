import { createTheme } from "@mui/material/styles";

export const appTheme = createTheme({
  palette: {
    primary: {
      main: "#e86f00",
    },

    secondary: {
      main: "#7c3aed",
    },

    background: {
      default: "#f5f3ee",
      paper: "#fcfbf8",
    },

    text: {
      primary: "#1f2937",
      secondary: "#6b7280",
    },

    success: {
      main: "#10b981",
    },

    error: {
      main: "#ef4444",
    },

    warning: {
      main: "#f59e0b",
    },
  },

  typography: {
    fontFamily: "'Inter', sans-serif",

    h1: {
      fontWeight: 800,
      color: "#1f2937",
    },

    h2: {
      fontWeight: 800,
      color: "#1f2937",
    },

    h3: {
      fontWeight: 700,
      color: "#1f2937",
    },

    h4: {
      fontWeight: 700,
      color: "#1f2937",
    },

    h5: {
      fontWeight: 700,
      color: "#1f2937",
    },

    h6: {
      fontWeight: 600,
      color: "#1f2937",
    },

    body1: {
      color: "#6b7280",
      fontSize: "15px",
    },

    body2: {
      color: "#9ca3af",
      fontSize: "14px",
    },

    button: {
      textTransform: "none",
      fontWeight: 600,
    },
  },

  shape: {
    borderRadius: 18,
  },

  components: {
    MuiCssBaseline: {
      styleOverrides: {
        body: {
          margin: 0,
          padding: 0,
          backgroundColor: "#f5f3ee",
          overflow: "hidden",
        },

        "*": {
          boxSizing: "border-box",
        },

        "*::-webkit-scrollbar": {
          width: "6px",
          height: "6px",
        },

        "*::-webkit-scrollbar-thumb": {
          backgroundColor: "#d1d5db",
          borderRadius: "10px",
        },

        "*::-webkit-scrollbar-track": {
          backgroundColor: "transparent",
        },
      },
    },

    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: 12,
          padding: "10px 18px",
          fontWeight: 600,
          boxShadow: "none",

          "&:hover": {
            boxShadow: "none",
            opacity: 0.95,
          },
        },

        contained: {
          backgroundColor: "#e86f00",
          color: "#fff",

          "&:hover": {
            backgroundColor: "#d65f00",
          },
        },
      },
    },

    MuiCard: {
      styleOverrides: {
        root: {
          borderRadius: 20,
          border: "1px solid #ece7df",
          boxShadow: "none",
          backgroundColor: "#fffaf5",
        },
      },
    },

    MuiDrawer: {
      styleOverrides: {
        paper: {
          backgroundColor: "#ffffff",
          borderRight: "1px solid #ece7df",
        },
      },
    },

    MuiTextField: {
      defaultProps: {
        variant: "outlined",
      },
    },

    MuiOutlinedInput: {
      styleOverrides: {
        root: {
          borderRadius: 14,
          backgroundColor: "#ffffff",

          "& fieldset": {
            borderColor: "#ece7df",
          },

          "&:hover fieldset": {
            borderColor: "#e86f00",
          },

          "&.Mui-focused fieldset": {
            borderColor: "#e86f00",
          },
        },
      },
    },

    MuiIconButton: {
      styleOverrides: {
        root: {
          borderRadius: 12,

          "&:hover": {
            backgroundColor: "#fff1e6",
            color: "#e86f00",
          },
        },
      },
    },

    MuiAvatar: {
      styleOverrides: {
        root: {
          backgroundColor: "#e86f00",
          fontWeight: 600,
        },
      },
    },
  },
});
