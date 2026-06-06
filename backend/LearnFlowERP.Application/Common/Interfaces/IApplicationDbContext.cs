using LearnFlowERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        // ----------------------------
        // TENANT
        // ----------------------------
        DbSet<Tenant> Tenants { get; }

        // ----------------------------
        // USER & AUTH
        // ----------------------------
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<UserRole> UserRoles { get; }

        DbSet<Permission> Permissions { get; }
        DbSet<RolePermission> RolePermissions { get; }
        DbSet<DesignationPermission> DesignationPermissions { get; }

        // ----------------------------
        // STUDENT MODULE
        // ----------------------------
        DbSet<Student> Students { get; }
        DbSet<Course> Courses { get; }
        DbSet<StudentCourse> StudentCourses { get; }
        DbSet<StudentAttendance> StudentAttendances { get; }


        // ----------------------------
        // FINANCE MODULE
        // ----------------------------
        DbSet<Fee> Fees { get; }
        DbSet<Payment> Payments { get; }
        DbSet<StudentScholarship> StudentScholarships { get; }
        DbSet<Refund> Refunds { get; }
        DbSet<PaymentAudit> PaymentAudits { get; }

        // ----------------------------
        // EMPLOYEE MODULE
        // ----------------------------
        DbSet<Employee> Employees { get; }
        DbSet<Designation> Designations { get; }
        DbSet<TeacherCourse> TeacherCourses { get; }
        DbSet<EmployeeAttendance> EmployeeAttendances { get; }

        // ----------------------------
        // AUDIT
        // ----------------------------
        DbSet<AuditLog> AuditLogs { get; }


        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<PasswordResetOtp> PasswordResetOtps { get; set; }

        // ----------------------------
        // COMMON
        // ----------------------------
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}