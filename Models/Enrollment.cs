namespace MultiTenantLMS.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public double Progress { get; set; } = 0; // percentage completed
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    }
}
