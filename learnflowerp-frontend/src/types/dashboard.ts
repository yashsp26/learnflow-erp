export type AdminDashboard = {
  totalStudents: number;
  totalEmployees: number;
  totalCourses: number;
  totalTeachers: number;
  studentsPresentToday: number;
  employeesPresentToday: number;
  totalFees: number;
  totalCollected: number;
  pendingFees: number;
};

export type TeacherDashboard = {
  assignedCourses: number;
  totalStudents: number;
  attendanceMarkedToday: number;
};

export type StudentDashboard = {
  totalCourses: number;
  attendancePercentage: number;
  pendingFees: number;
};
