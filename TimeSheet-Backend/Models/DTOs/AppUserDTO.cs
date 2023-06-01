using System.ComponentModel.DataAnnotations;

namespace TimeSheet_Backend.Models.DTOs
{
    public class RegisterUserDTO : LoginUserDTO
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters long")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters long")]
        public string LastName { get; set; }
        public string Role { get; set; }
    }

    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,50}$")]
        public string Password { get; set; }
    }

    public class ManagerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
