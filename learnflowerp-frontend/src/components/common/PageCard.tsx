import {
  Card,
  CardContent,
} from "@mui/material";

type Props = {
  children: React.ReactNode;
};

export default function PageCard({
  children,
}: Props) {
  return (
    <Card
      elevation={0}
      sx={{
        height: "100%",
      }}
    >
      <CardContent sx={{ p: { xs: 2, md: 3 }, "&:last-child": { pb: { xs: 2, md: 3 } } }}>
        {children}
      </CardContent>
    </Card>
  );
}
