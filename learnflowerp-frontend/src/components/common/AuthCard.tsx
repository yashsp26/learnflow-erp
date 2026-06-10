import {
  Card,
  CardContent,
  Typography,
} from "@mui/material";

type Props = {
  title: string;
  subtitle: string;
  children: React.ReactNode;
};

export default function AuthCard({
  title,
  subtitle,
  children,
}: Props) {
  return (
    <Card
      elevation={0}
      sx={{
        width: 420,
        p: 2,
        borderRadius: "20px",
        border: "1px solid #ece7df",
      }}
    >
      <CardContent>
        <Typography
          variant="h4"
          sx={{
            fontWeight: 700,
            mb: 1,
          }}
        >
          {title}
        </Typography>

        <Typography
          sx={{
            color: "#6b7280",
            mb: 3,
          }}
        >
          {subtitle}
        </Typography>

        {children}
      </CardContent>
    </Card>
  );
}