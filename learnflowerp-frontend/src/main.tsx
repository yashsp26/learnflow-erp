import React from "react";
import ReactDOM from "react-dom/client";
import "@fontsource/inter/400.css";
import "@fontsource/inter/500.css";
import "@fontsource/inter/600.css";
import "@fontsource/inter/700.css";
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
              background: "#FFFFFF",
              color: "#172033",
              border: "1px solid #E2E8F0",
              borderRadius: "10px",
              padding: "12px 16px",
            },

            success: {
              iconTheme: {
                primary: "#16A34A",
                secondary: "#fff",
              },
            },

            error: {
              iconTheme: {
                primary: "#DC2626",
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
