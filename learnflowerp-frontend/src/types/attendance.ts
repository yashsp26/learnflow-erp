export const attendanceStatuses = ["Present", "Absent", "Leave", "HalfDay"] as const;
export type AttendanceStatus = (typeof attendanceStatuses)[number];

export interface StudentAttendanceRecord {
  studentAttendanceId: number;
  studentId: number;
  courseId: number;
  markedByEmployeeId: number;
  tenantId: number;
  attendanceDate: string;
  status: AttendanceStatus;
  createdAt: string;
}

export interface StudentAttendanceHistory {
  date: string;
  status: AttendanceStatus;
  courseName: string;
}

export interface EmployeeAttendance {
  employeeId: number;
  employeeName: string;
  attendanceDate: string;
  status: AttendanceStatus;
}
