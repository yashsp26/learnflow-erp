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
        alignItems: "center",
        mb: 3,
      }}
    >
      <Box>
        <Typography
          variant="h4"
          sx={{
            fontWeight: 700,
          }}
        >
          {title}
        </Typography>

        {subtitle && (
          <Typography
            color="text.secondary"
          >
            {subtitle}
          </Typography>
        )}
      </Box>

      {action}
    </Box>
  );
}