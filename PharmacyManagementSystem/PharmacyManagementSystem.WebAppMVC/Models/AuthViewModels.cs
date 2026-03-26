using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.WebAppMVC.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class SignupViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
