namespace MultiTenantLMS.DTOs
{
    public class CourseCreateDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int TenantId { get; set; }
    }
}
