namespace MultiTenantLMS.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Domain { get; set; } = null!; // optional

        public ICollection<User>? Users { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
