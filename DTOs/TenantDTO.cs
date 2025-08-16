namespace MultiTenantLMS.DTOs
{
    public class TenantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Domain { get; set; } = null!;
    }
}
