using Microsoft.EntityFrameworkCore;
using MultiTenantLMS.Models;

namespace MultiTenantLMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationships
            modelBuilder.Entity<Tenant>()
                .HasMany(t => t.Users)
                .WithOne(u => u.Tenant)
                .HasForeignKey(u => u.TenantId);

            modelBuilder.Entity<Tenant>()
                .HasMany(t => t.Courses)
                .WithOne(c => c.Tenant)
                .HasForeignKey(c => c.TenantId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Enrollments)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Enrollments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);

            // ===== Seed Data =====
            modelBuilder.Entity<Tenant>().HasData(
                new Tenant { Id = 1, Name = "Tenant A", Domain = "tenantA.com" },
                new Tenant { Id = 2, Name = "Tenant B", Domain = "tenantB.com" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "adminA", PasswordHash = "hashedpassA", Role = "Admin", Email = "adminA@tenantA.com", TenantId = 1 },
                new User { Id = 2, Username = "studentA", PasswordHash = "hashedpassA", Role = "Student", Email = "studentA@tenantA.com", TenantId = 1 },
                new User { Id = 3, Username = "adminB", PasswordHash = "hashedpassB", Role = "Admin", Email = "adminB@tenantB.com", TenantId = 2 },
                new User { Id = 4, Username = "studentB", PasswordHash = "hashedpassB", Role = "Student", Email = "studentB@tenantB.com", TenantId = 2 }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "C# Basics", Description = "Intro to C#", TenantId = 1 },
                new Course { Id = 2, Title = "ASP.NET Core", Description = "Learn ASP.NET Core", TenantId = 1 },
                new Course { Id = 3, Title = "Java Basics", Description = "Intro to Java", TenantId = 2 }
            );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { Id = 1, UserId = 2, CourseId = 1, Progress = 0, EnrolledAt = DateTime.Now },
                new Enrollment { Id = 2, UserId = 4, CourseId = 3, Progress = 0, EnrolledAt = DateTime.Now }
            );
        }
    }
}
