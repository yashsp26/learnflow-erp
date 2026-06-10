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
        borderRadius: "20px",
        border:
          "1px solid #ece7df",
      }}
    >
      <CardContent>
        {children}
      </CardContent>
    </Card>
  );
}