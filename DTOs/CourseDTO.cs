namespace MultiTenantLMS.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int TenantId { get; set; }
    }
}
