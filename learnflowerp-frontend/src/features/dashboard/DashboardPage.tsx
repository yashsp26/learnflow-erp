import { Box, Card, CardContent, Grid, Typography } from "@mui/material";
import AssessmentOutlinedIcon from "@mui/icons-material/AssessmentOutlined";

import { useEffect, useState } from "react";

import toast from "react-hot-toast";

import {
  getAdminDashboardApi,
  getStudentDashboardApi,
  getTeacherDashboardApi,
} from "../../api/dashboardApi";
import PageHeader from "../../components/common/PageHeader";
import { useAppSelector } from "../../app/hooks";

type DashboardData = Record<string, number>;

const formatLabel = (key: string) =>
  key.replace(/([A-Z])/g, " $1").replace(/^./, (value) => value.toUpperCase());

const formatValue = (key: string, value: number) =>
  key.toLowerCase().includes("fee") || key.toLowerCase().includes("collected")
    ? new Intl.NumberFormat("en-IN", {
        style: "currency",
        currency: "INR",
        maximumFractionDigits: 0,
      }).format(value)
    : value.toLocaleString();

export default function DashboardPage() {
  const permissions = useAppSelector((state) => state.permissions.items);
  const [data, setData] = useState<DashboardData>({});

  useEffect(() => {
    const loadDashboard = async () => {
      try {
        const response = permissions.includes("CreateUser")
          ? await getAdminDashboardApi()
          : permissions.includes("AssignTeacherCourse")
            ? await getTeacherDashboardApi()
            : await getStudentDashboardApi();

        setData(response.data ?? {});
      } catch {
        toast.error("Failed to load dashboard");
      }
    };

    if (permissions.length) {
      void loadDashboard();
    }
  }, [permissions]);

  return (
    <Box>
      <PageHeader title="Dashboard" subtitle="Live overview of your ERP" />

      <Grid container spacing={3}>
        {Object.entries(data).map(([key, value]) => (
          <Grid
            key={key}
            size={{ xs: 12, md: 4 }}
          >
            <Card elevation={0} sx={{ height: "100%" }}>
              <CardContent
                sx={{
                  p: 3,
                  "&:last-child": { pb: 3 },
                }}
              >
                <Box sx={{ display: "flex", alignItems: "center", justifyContent: "space-between", mb: 3 }}>
                  <Typography variant="subtitle2" color="text.secondary">{formatLabel(key)}</Typography>
                  <Box sx={{ display: "grid", placeItems: "center", width: 40, height: 40, borderRadius: 1, color: "primary.main", bgcolor: "primary.light" }}>
                    <AssessmentOutlinedIcon fontSize="small" />
                  </Box>
                </Box>

                <Typography
                  variant="h3"
                  sx={{
                    fontWeight: 700,
                  }}
                >
                  {formatValue(key, value)}
                </Typography>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>
    </Box>
  );
}
