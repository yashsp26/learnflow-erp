using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Infrastructure.Persistence.AppDbContext
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            ICurrentUserService currentUser,
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _currentUser = currentUser;
            _httpContextAccessor = httpContextAccessor;
        }

        // ----------------------------
        // DbSets
        // ----------------------------
        public DbSet<Tenant> Tenants => Set<Tenant>();

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<StudentCourse> StudentCourses => Set<StudentCourse>();

        public DbSet<Fee> Fees => Set<Fee>();
        public DbSet<Payment> Payments => Set<Payment>();

        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        public DbSet<Designation> Designations => Set<Designation>();
        public DbSet<TeacherCourse> TeacherCourses => Set<TeacherCourse>();
        public DbSet<StudentAttendance> StudentAttendances => Set<StudentAttendance>();
        public DbSet<EmployeeAttendance> EmployeeAttendances => Set<EmployeeAttendance>();

        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<PasswordResetOtp> PasswordResetOtps { get; set; }

        // ----------------------------
        // Model Configuration
        // ----------------------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyGlobalTenantFilter(modelBuilder);

            // ============================
            // TENANT
            // ============================
            modelBuilder.Entity<Tenant>()
                .HasIndex(t => t.Code)
                .IsUnique();

            // ============================
            // USER ↔ TENANT
            // ============================
            modelBuilder.Entity<User>()
                .HasOne(u => u.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Tenant)
                .WithMany(t => t.Students)
                .HasForeignKey(s => s.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Tenant)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Tenant)
                .WithMany(t => t.Employees)
                .HasForeignKey(e => e.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            // ============================
            // USER ROLE (Many-to-Many)
            // ============================
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId, ur.TenantId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // ============================
            // ROLE PERMISSION
            // ============================
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany()
                .HasForeignKey(rp => rp.PermissionId);

            // ============================
            // STUDENT COURSE
            // ============================
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId, sc.TenantId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);

            // ============================
            // ONE-TO-ONE
            // ============================
            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne(u => u.Employee)
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ============================
            // RELATIONS
            // ============================
            modelBuilder.Entity<Fee>()
                .HasOne(f => f.Student)
                .WithMany(s => s.Fees)
                .HasForeignKey(f => f.StudentId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Fee)
                .WithMany(f => f.Payments)
                .HasForeignKey(p => p.FeeId);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Designation)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DesignationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TeacherCourse>()
                .HasKey(x => new
                {
                    x.EmployeeId,
                    x.CourseId,
                    x.TenantId
                });

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(x => x.Employee)
                .WithMany(x => x.TeacherCourses)
                .HasForeignKey(x => x.EmployeeId);

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(x => x.Course)
                .WithMany(x => x.TeacherCourses)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<StudentAttendance>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Attendances)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentAttendance>()
                .HasOne(x => x.Course)
                .WithMany(x => x.Attendances)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentAttendance>()
                .HasOne(x => x.MarkedByEmployee)
                .WithMany(x => x.MarkedAttendances)
                .HasForeignKey(x => x.MarkedByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeAttendance>()
                .HasOne(x => x.Employee)
                .WithMany(x => x.Attendances)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // ============================
            // AUDIT LOG
            // ============================
            modelBuilder.Entity<AuditLog>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<AuditLog>()
                .HasOne<Tenant>()
                .WithMany()
                .HasForeignKey(a => a.TenantId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // ============================
            // REFRESH TOKEN
            // ============================
            modelBuilder.Entity<RefreshToken>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .IsRequired(false); // ✅ FIXED WARNING

            // ============================
            // DECIMAL PRECISION (FIX)
            // ============================
            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Fee>()
                .Property(f => f.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.AmountPaid)
                .HasPrecision(18, 2);

            // ============================
            // INDEXES
            // ============================
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.Email, u.TenantId })
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => new { s.EnrollmentNo, s.TenantId })
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => new { e.EmpCode, e.TenantId })
                .IsUnique();

            modelBuilder.Entity<Course>()
                .HasIndex(c => new { c.CourseCode, c.TenantId })
                .IsUnique();
        }

        // ============================
        // GLOBAL TENANT FILTER (FIXED)
        // ============================
        private void ApplyGlobalTenantFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(u =>
                    _currentUser.TenantId == null ||
                    (u.TenantId == _currentUser.TenantId && u.IsActive));

            modelBuilder.Entity<Student>()
                .HasQueryFilter(s =>
                    _currentUser.TenantId == null ||
                    (s.TenantId == _currentUser.TenantId && s.IsActive));

            modelBuilder.Entity<Course>()
                .HasQueryFilter(c =>
                    _currentUser.TenantId == null ||
                    (c.TenantId == _currentUser.TenantId && c.IsActive));

            modelBuilder.Entity<Employee>()
                .HasQueryFilter(e =>
                    _currentUser.TenantId == null ||
                    (e.TenantId == _currentUser.TenantId && e.IsActive));

            modelBuilder.Entity<Fee>()
                .HasQueryFilter(f =>
                    _currentUser.TenantId == null ||
                    f.TenantId == _currentUser.TenantId);

            modelBuilder.Entity<Payment>()
                .HasQueryFilter(p =>
                    _currentUser.TenantId == null ||
                    p.TenantId == _currentUser.TenantId);

            modelBuilder.Entity<UserRole>()
                .HasQueryFilter(ur =>
                    _currentUser.TenantId == null ||
                    ur.TenantId == _currentUser.TenantId);

            modelBuilder.Entity<StudentCourse>()
                .HasQueryFilter(sc =>
                    _currentUser.TenantId == null ||
                    sc.TenantId == _currentUser.TenantId);

            modelBuilder.Entity<AuditLog>()
                .HasQueryFilter(a =>
                    _currentUser.TenantId == null ||
                    a.TenantId == _currentUser.TenantId);

            modelBuilder.Entity<TeacherCourse>()
                .HasQueryFilter(x =>
                    _currentUser.TenantId == null ||
                    x.TenantId == _currentUser.TenantId);

            modelBuilder.Entity<StudentAttendance>()
                .HasQueryFilter(x =>
                    _currentUser.TenantId == null ||
                    x.TenantId == _currentUser.TenantId);

            modelBuilder.Entity<EmployeeAttendance>()
                .HasQueryFilter(x =>
                    _currentUser.TenantId == null ||
                    x.TenantId == _currentUser.TenantId);

            modelBuilder.Entity<Designation>()
                .HasQueryFilter(x =>
                    _currentUser.TenantId == null ||
                    x.TenantId == _currentUser.TenantId);
        }

        // ============================
        // AUDIT LOGGING
        // ============================
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditEntries = new List<AuditLog>();

            var tenantId = _currentUser.TenantId;
            var userId = _currentUser.UserId;

            var correlationId = _httpContextAccessor.HttpContext?
                .Items["CorrelationId"]?.ToString();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog)
                    continue;

                if (entry.State == EntityState.Added ||
                    entry.State == EntityState.Modified ||
                    entry.State == EntityState.Deleted)
                {
                    var audit = new AuditLog
                    {
                        TableName = entry.Entity.GetType().Name,
                        Action = entry.State.ToString(),
                        TenantId = tenantId,
                        UserId = userId,
                        CreatedAt = DateTime.Now,
                        CorrelationId = correlationId
                    };

                    var key = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey());

                    if (key?.CurrentValue != null)
                        audit.RecordId = Convert.ToInt64(key.CurrentValue);

                    var excluded = new[] { "PasswordHash", "Password", "RefreshToken", "SecurityStamp" };

                    if (entry.State != EntityState.Added)
                    {
                        audit.OldValues = System.Text.Json.JsonSerializer.Serialize(
                            entry.OriginalValues.Properties
                                .Where(p => !excluded.Contains(p.Name))
                                .ToDictionary(p => p.Name, p => entry.OriginalValues[p])
                        );
                    }

                    if (entry.State != EntityState.Deleted)
                    {
                        audit.NewValues = System.Text.Json.JsonSerializer.Serialize(
                            entry.CurrentValues.Properties
                                .Where(p => !excluded.Contains(p.Name))
                                .ToDictionary(p => p.Name, p => entry.CurrentValues[p])
                        );
                    }

                    auditEntries.Add(audit);
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            if (auditEntries.Any())
            {
                AuditLogs.AddRange(auditEntries);
                await base.SaveChangesAsync(cancellationToken);
            }

            return result;
        }
    }
}