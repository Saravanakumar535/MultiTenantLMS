namespace MultiTenantLMS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "Student"; // Admin, Instructor, Student
        public int TenantId { get; set; }
        public Tenant? Tenant { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public string Email { get; internal set; }
    }
}
