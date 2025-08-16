namespace MultiTenantLMS.DTOs
{
    public class UserCreateDTO
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public int TenantId { get; set; }
    }
}
