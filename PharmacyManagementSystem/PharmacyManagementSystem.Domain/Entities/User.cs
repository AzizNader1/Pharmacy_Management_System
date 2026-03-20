using PharmacyManagementSystem.Domain.Enums;

namespace PharmacyManagementSystem.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; // Store hashed password
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public UserRoles UserRole { get; set; }

        public ICollection<Sale> Sales { get; set; } = [];
    }
}
