using PharmacyManagementSystem.Domain.Enums;

namespace PharmacyManagementSystem.Application.DTOs.UserDTOs
{
    public class GetUserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public UserRoles UserRole { get; set; }
        public string ResponseMessage { get; set; } = string.Empty;
    }
}
