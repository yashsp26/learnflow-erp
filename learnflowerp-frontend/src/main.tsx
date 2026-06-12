import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";

import { Provider } from "react-redux";
import { ThemeProvider } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";
import { Toaster } from "react-hot-toast";
import { appTheme } from "./theme/theme";
import { store } from "./app/store";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <Provider store={store}>
      <ThemeProvider theme={appTheme}>
        <CssBaseline />
        <Toaster
          position="top-right"
          toastOptions={{
            duration: 3000,

            style: {
              background: "#ffffff",
              color: "#1f2937",
              border: "1px solid #ece7df",
              borderRadius: "14px",
              padding: "14px 16px",
            },

            success: {
              iconTheme: {
                primary: "#10b981",
                secondary: "#fff",
              },
            },

            error: {
              iconTheme: {
                primary: "#ef4444",
                secondary: "#fff",
              },
            },
          }}
        />
        <App />
      </ThemeProvider>
    </Provider>
  </React.StrictMode>
);
