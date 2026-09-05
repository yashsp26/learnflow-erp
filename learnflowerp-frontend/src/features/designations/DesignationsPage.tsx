import {
  Box,
  Button,
  Card,
  CardContent,
  IconButton,
} from "@mui/material";

import {
  VisibilityOutlined,
  EditOutlined,
  DeleteOutlined,
  AddOutlined,
} from "@mui/icons-material";

import { DataGrid } from "@mui/x-data-grid";

import type { GridColDef } from "@mui/x-data-grid";

import { useEffect, useState } from "react";

import toast from "react-hot-toast";

import PageHeader from "../../components/common/PageHeader";
import SearchBar from "../../components/common/SearchBar";

import {
  getDesignationsApi,
  deleteDesignationApi,
} from "../../api/designationApi";

import type { Designation } from "../../types/designation";

import CreateDesignationDialog from "./CreateDesignationDialog";
import EditDesignationDialog from "./EditDesignationDialog";
import DesignationDetailsDialog from "./DesignationDetailsDialog";

export default function DesignationsPage() {
  const [designations, setDesignations] = useState<Designation[]>([]);

  const [selectedId, setSelectedId] =
    useState<number | null>(null);

  const [loading, setLoading] =
    useState(false);

  const [openCreate, setOpenCreate] =
    useState(false);

  const [openEdit, setOpenEdit] =
    useState(false);

  const [openDetails, setOpenDetails] =
    useState(false);

  const fetchDesignations = async () => {
    try {
      setLoading(true);

      const response =
        await getDesignationsApi();

      setDesignations(
        response.data ?? []
      );
    } catch {
      toast.error(
        "Failed to load designations"
      );
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    const loadDesignations =
      async () => {
        await fetchDesignations();
      };

    void loadDesignations();
  }, []);

  const handleDelete = async (
    designationId: number
  ) => {
    const confirmed =
      window.confirm(
        "Delete this designation?"
      );

    if (!confirmed) {
      return;
    }

    try {
      await deleteDesignationApi(
        designationId
      );

      toast.success(
        "Designation deleted successfully"
      );

      await fetchDesignations();
    } catch {
      toast.error(
        "Failed to delete designation"
      );
    }
  };

  const columns: GridColDef[] = [
    {
      field: "designationId",
      headerName: "ID",
      width: 100,
    },
    {
      field: "name",
      headerName: "Designation Name",
      flex: 1,
    },
    {
      field: "actions",
      headerName: "Actions",
      width: 180,
      sortable: false,

      renderCell: (params) => (
        <Box>
          <IconButton
            onClick={() => {
              setSelectedId(
                params.row.designationId
              );

              setOpenDetails(true);
            }}
          >
            <VisibilityOutlined />
          </IconButton>

          <IconButton
            onClick={() => {
              setSelectedId(
                params.row.designationId
              );

              setOpenEdit(true);
            }}
          >
            <EditOutlined />
          </IconButton>

          <IconButton
            color="error"
            onClick={() =>
              handleDelete(
                params.row.designationId
              )
            }
          >
            <DeleteOutlined />
          </IconButton>
        </Box>
      ),
    },
  ];

  return (
    <Box>
      <PageHeader
        title="Designations"
      />

      <Box
        sx={{
          marginBottom: 3,
        }}
      >
        <SearchBar
          placeholder="Search designations..."
        />
      </Box>

      <Box
        sx={{
          marginBottom: 3,
        }}
      >
        <Button
          startIcon={<AddOutlined />}
          variant="contained"
          onClick={() =>
            setOpenCreate(true)
          }
        >
          Add Designation
        </Button>
      </Box>

      <Card
        elevation={0}
        sx={{
          borderRadius: "20px",
          border:
            "1px solid #ece7df",
        }}
      >
        <CardContent>
          <Box
            sx={{
              height: 600,
            }}
          >
            <DataGrid
              rows={designations}
              columns={columns}
              loading={loading}
              getRowId={(row) =>
                row.designationId
              }
              disableRowSelectionOnClick
              pageSizeOptions={[
                10,
                20,
                50,
              ]}
              sx={{
                border: "none",

                "& .MuiDataGrid-columnHeaders":
                  {
                    backgroundColor:
                      "#f9fafb",
                  },

                "& .MuiDataGrid-row:hover":
                  {
                    backgroundColor:
                      "#fffaf5",
                  },
              }}
            />
          </Box>
        </CardContent>
      </Card>

      <CreateDesignationDialog
        open={openCreate}
        onClose={() =>
          setOpenCreate(false)
        }
        onSuccess={
          fetchDesignations
        }
      />

      <EditDesignationDialog
        open={openEdit}
        designationId={
          selectedId
        }
        onClose={() =>
          setOpenEdit(false)
        }
        onSuccess={
          fetchDesignations
        }
      />

      <DesignationDetailsDialog
        open={openDetails}
        designationId={
          selectedId
        }
        onClose={() =>
          setOpenDetails(false)
        }
      />
    </Box>
  );
}