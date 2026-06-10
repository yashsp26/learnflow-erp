import { Box, Typography } from "@mui/material";

type Props = {
  title: string;
  subtitle?: string;
};

export default function EmptyState({ title, subtitle }: Props) {
  return (
    <Box
      sx={{
        textAlign: "center",
        py: 8,
      }}
    >
      <Typography variant="h6" sx={{ fontWeight: 600 }}>
        {title}
      </Typography>

      {subtitle && <Typography color="text.secondary">{subtitle}</Typography>}
    </Box>
  );
}
