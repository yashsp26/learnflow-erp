import {
  Box,
  Typography,
} from "@mui/material";

type Props = {
  title: string;
  subtitle?: string;
  action?: React.ReactNode;
};

export default function PageHeader({
  title,
  subtitle,
  action,
}: Props) {
  return (
    <Box
      sx={{
        display: "flex",
        justifyContent:
          "space-between",
        alignItems: { xs: "flex-start", sm: "center" },
        gap: 2,
        flexWrap: "wrap",
        mb: 4,
      }}
    >
      <Box>
        <Typography
          variant="h3"
        >
          {title}
        </Typography>

        {subtitle && (
          <Typography
            variant="body2"
            color="text.secondary"
            sx={{ mt: 0.5 }}
          >
            {subtitle}
          </Typography>
        )}
      </Box>

      {action}
    </Box>
  );
}
