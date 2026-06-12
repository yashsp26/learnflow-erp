import {
  Box,
  Card,
  CardContent,
  Grid,
  Typography,
} from "@mui/material";

import PageHeader from "../../components/common/PageHeader";
import SearchBar from "../../components/common/SearchBar";

export default function DashboardPage() {
  const cards = [
    {
      title: "Students",
      value: "120",
    },
    {
      title: "Courses",
      value: "18",
    },
    {
      title: "Revenue",
      value: "$42,500",
    },
  ];

  return (
    <Box>
      <PageHeader
        title="Dashboard management"
        subtitle="Overview of ERP statistics"
      />

      <SearchBar placeholder="Search dashboard..." />

      <Grid container spacing={3}>
        {cards.map((card) => (
          <Grid
            key={card.title}
            size={{ xs: 12, md: 4 }}
          >
            <Card
              elevation={0}
              sx={{
                backgroundColor: "#fffaf5",
                border: "1px solid #f1e8dc",
                borderRadius: "20px",
              }}
            >
              <CardContent
                sx={{
                  padding: 4,
                }}
              >
                <Typography
                  sx={{
                    color: "#6b7280",
                    marginBottom: 1,
                  }}
                >
                  {card.title}
                </Typography>

                <Typography
                  variant="h4"
                  sx={{
                    fontWeight: 700,
                  }}
                >
                  {card.value}
                </Typography>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>
    </Box>
  );
}