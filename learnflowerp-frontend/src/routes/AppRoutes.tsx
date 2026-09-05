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
import EmployeesPage from "../features/employees/EmployeesPage";
import StudentCoursesPage from "../features/student-courses/StudentCoursesPage";
import TeacherCoursesPage from "../features/teacher-courses/TeacherCoursesPage";
import StudentAttendancePage from "../features/student-attendance/StudentAttendancePage";
import EmployeeAttendancePage from "../features/employee-attendance/EmployeeAttendancePage";
import FeesPage from "../features/fees/FeesPage";
import PaymentsPage from "../features/payments/PaymentsPage";

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

          <Route path="/employees" element={<EmployeesPage />} />

          <Route path="/students" element={<StudentsPage />} />

          <Route path="/courses" element={<CoursesPage />} />

          <Route path="/student-courses" element={<StudentCoursesPage />} />

          <Route path="/teacher-courses" element={<TeacherCoursesPage />} />

          <Route path="/student-attendance" element={<StudentAttendancePage />} />

          <Route path="/employee-attendance" element={<EmployeeAttendancePage />} />

          <Route path="/fees" element={<FeesPage />} />

          <Route path="/payments" element={<PaymentsPage />} />

          <Route path="/designations" element={<DesignationsPage />} />

          <Route path="/permissions" element={<PermissionsPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
