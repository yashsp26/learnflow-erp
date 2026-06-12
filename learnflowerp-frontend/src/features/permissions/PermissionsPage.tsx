import {
  Box,
  Card,
  CardContent,
  Tab,
  Tabs,
} from "@mui/material";

import { useState } from "react";

import PageHeader from "../../components/common/PageHeader";

import RolePermissionsTab from "./components/RolePermissionsTab";
import DesignationPermissionsTab from "./components/DesignationPermissionsTab";

export default function PermissionsPage() {
  const [tab, setTab] =
    useState(0);

  return (
    <Box>
      <PageHeader
        title="Permissions"
      />

      <Card>
        <CardContent>
          <Tabs
            value={tab}
            onChange={(_, value) =>
              setTab(value)
            }
          >
            <Tab label="Roles" />

            <Tab
              label="Designations"
            />
          </Tabs>

          <Box
            sx={{
              marginTop: 3,
            }}
          >
            {tab === 0 && (
              <RolePermissionsTab />
            )}

            {tab === 1 && (
              <DesignationPermissionsTab />
            )}
          </Box>
        </CardContent>
      </Card>
    </Box>
  );
}