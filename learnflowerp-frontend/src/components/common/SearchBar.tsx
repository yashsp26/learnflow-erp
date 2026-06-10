import {
  Box,
  InputBase,
} from "@mui/material";

import SearchIcon from "@mui/icons-material/Search";

type Props = {
  value?: string;
  onChange?: (
    value: string
  ) => void;
  placeholder?: string;
};

export default function SearchBar({
  value,
  onChange,
  placeholder = "Search...",
}: Props) {
  return (
    <Box
      sx={{
        display: "flex",
        alignItems: "center",
        backgroundColor: "#f9fafb",
        border: "1px solid #ece7df",
        borderRadius: "14px",
        padding: "10px 16px",
      }}
    >
      <SearchIcon
        sx={{
          color: "#9ca3af",
          mr: 1,
        }}
      />

      <InputBase
        fullWidth
        value={value}
        placeholder={placeholder}
        onChange={(e) =>
          onChange?.(e.target.value)
        }
      />
    </Box>
  );
}