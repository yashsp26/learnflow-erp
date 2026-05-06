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

        // ----------------------------
        // STUDENT MODULE
        // ----------------------------
        DbSet<Student> Students { get; }
        DbSet<Course> Courses { get; }
        DbSet<StudentCourse> StudentCourses { get; }

        // ----------------------------
        // FINANCE MODULE
        // ----------------------------
        DbSet<Fee> Fees { get; }
        DbSet<Payment> Payments { get; }

        // ----------------------------
        // EMPLOYEE MODULE
        // ----------------------------
        DbSet<Employee> Employees { get; }
        DbSet<Attendance> Attendances { get; }

        // ----------------------------
        // AUDIT
        // ----------------------------
        DbSet<AuditLog> AuditLogs { get; }


        DbSet<RefreshToken> RefreshTokens { get; }

        // ----------------------------
        // COMMON
        // ----------------------------
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}