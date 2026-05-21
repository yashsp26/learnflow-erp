import { Box, Button, Card, CardContent, Typography } from "@mui/material";

import { DataGrid } from "@mui/x-data-grid";

import type { GridColDef } from "@mui/x-data-grid";

import { useEffect, useState } from "react";

import SearchBar from "../../components/common/SearchBar";

import { getUsersApi } from "../../api/userApi";

import type { User } from "../../types/user";

import CreateUserDialog from "./CreateUserDialog";
import { appTheme } from "../../theme/theme";
import toast from "react-hot-toast";

export default function UsersPage() {
  const [users, setUsers] = useState<User[]>([]);

  const [loading, setLoading] = useState(true);

  const [openCreate, setOpenCreate] = useState(false);

  const fetchUsers = async (): Promise<void> => {
    try {
      setLoading(true);

      const response = await getUsersApi();

      setUsers(response.Data);
    } catch {
      toast.error("Failed to fetch users");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    const loadUsers = async () => {
      await fetchUsers();
    };

    loadUsers();
  }, []);

  const columns: GridColDef[] = [
    {
      field: "userId",
      headerName: "ID",
      width: 100,
    },
    {
      field: "username",
      headerName: "Username",
      flex: 1,
    },
    {
      field: "email",
      headerName: "Email",
      flex: 1,
    },
  ];

  return (
    <Box>
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          marginBottom: 3,
        }}
      >
        <Typography variant="h4">Users</Typography>

        <Button
          variant="contained"
          onClick={() => setOpenCreate(true)}
          sx={{
            backgroundColor: appTheme.palette.primary.main,
          }}
        >
          Add User
        </Button>
      </Box>

      <SearchBar placeholder="Search users..." />

      <Card
        elevation={0}
        sx={{
          borderRadius: "20px",
          border: "1px solid #ece7df",
        }}
      >
        <CardContent>
          <Box
            sx={{
              height: 500,
            }}
          >
            <DataGrid
              rows={users}
              columns={columns}
              loading={loading}
              getRowId={(row) => row.userId}
              disableRowSelectionOnClick
              pageSizeOptions={[5, 10, 20]}
              sx={{
                border: "none",
                backgroundColor: "#fcfbf8",

                "& .MuiDataGrid-columnHeaders": {
                  backgroundColor: "#f9fafb",
                  borderBottom: "1px solid #ece7df",
                },

                "& .MuiDataGrid-cell": {
                  borderBottom: "1px solid #f3f4f6",
                },

                "& .MuiDataGrid-footerContainer": {
                  borderTop: "1px solid #ece7df",
                },

                "& .MuiDataGrid-row:hover": {
                  backgroundColor: "#fffaf5",
                },
              }}
            />
          </Box>
        </CardContent>
      </Card>

      <CreateUserDialog
        open={openCreate}
        onClose={() => setOpenCreate(false)}
        onSuccess={fetchUsers}
      />
    </Box>
  );
}
