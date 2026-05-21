import {
  Box,
  InputBase,
} from "@mui/material";

import SearchIcon from "@mui/icons-material/Search";

type Props = {
  placeholder?: string;
};

export default function SearchBar({
  placeholder,
}: Props) {
  return (
    <Box
      sx={{
        display: "flex",
        alignItems: "center",
        backgroundColor: "#f3f4f6",
        borderRadius: "12px",
        padding: "10px 16px",
        marginBottom: 3,
      }}
    >
      <SearchIcon
        sx={{
          color: "#9ca3af",
          marginRight: 1,
        }}
      />

      <InputBase
        placeholder={
          placeholder || "Search..."
        }
        sx={{
          width: "100%",
        }}
      />
    </Box>
  );
}