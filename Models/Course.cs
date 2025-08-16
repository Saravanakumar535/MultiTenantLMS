namespace MultiTenantLMS.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int TenantId { get; set; }
        public Tenant? Tenant { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    }
}
