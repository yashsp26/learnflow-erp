import {
  Box,
  Typography,
} from "@mui/material";

type Props = {
  title: string;
  tabs?: string[];
};

export default function PageHeader({
  title,
  tabs,
}: Props) {
  return (
    <Box sx={{ marginBottom: 5 }}>
      <Typography
        variant="h4"
        sx={{
          marginBottom: 3,
        }}
      >
        {title}
      </Typography>

      {tabs && (
        <Box
          sx={{
            display: "flex",
            gap: 4,
            borderBottom: "1px solid #ece7df",
            paddingBottom: 1.5,
          }}
        >
          {tabs.map((tab, index) => (
            <Typography
              key={index}
              sx={{
                fontWeight: index === 0 ? 700 : 500,
                color:
                  index === 0
                    ? "#1f2937"
                    : "#9ca3af",
                borderBottom:
                  index === 0
                    ? "2px solid #e86f00"
                    : "none",
                paddingBottom: 1,
                cursor: "pointer",
              }}
            >
              {tab}
            </Typography>
          ))}
        </Box>
      )}
    </Box>
  );
}