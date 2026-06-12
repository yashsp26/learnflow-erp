import { BrowserRouter, Routes, Route } from "react-router-dom";

import LoginPage from "../features/auth/LoginPage";
import DashboardPage from "../features/dashboard/DashboardPage";
import UsersPage from "../features/users/UsersPage";
import StudentsPage from "../features/students/StudentsPage";
import CoursesPage from "../features/courses/CoursesPage";
import ForgotPasswordPage from "../features/auth/ForgotPasswordPage";
import ResetPasswordPage from "../features/auth/ResetPasswordPage";
import AppLayout from "../components/layout/AppLayout";
import DesignationsPage from "../features/designations/DesignationsPage";
import PermissionsPage from "../features/permissions/PermissionsPage";

export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPage />} />

        <Route path="/forgot-password" element={<ForgotPasswordPage />} />

        <Route path="/reset-password" element={<ResetPasswordPage />} />

        <Route element={<AppLayout />}>
          <Route path="/dashboard" element={<DashboardPage />} />

          <Route path="/users" element={<UsersPage />} />

          <Route path="/students" element={<StudentsPage />} />

          <Route path="/courses" element={<CoursesPage />} />

          <Route path="/designations" element={<DesignationsPage />} />

          <Route path="/permissions" element={<PermissionsPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
