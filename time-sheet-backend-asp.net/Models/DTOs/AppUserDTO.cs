using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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

    public class GetUserDTO
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters long")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters long")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class EditUserDTO
    {
        private string _NewEmail;
        private string _NewEmailConfirmation;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string OldEmail { get; set; }
        [AllowNull]
        [EmailAddress]
        public string NewEmail { get { return _NewEmail; } set { _NewEmail = string.IsNullOrWhiteSpace(value) ? null : value; } }
        [AllowNull]
        [EmailAddress]
        [Compare("NewEmail", ErrorMessage = "The email and confirmation do not match.")]
        public string NewEmailConfirmation { get { return _NewEmailConfirmation; } set { _NewEmailConfirmation = string.IsNullOrWhiteSpace(value) ? null : value; } }
        [Required]
        public string OldPassword { get; set; }
        [AllowNull]
        public string NewPassword { get; set; }
        [AllowNull]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation do not match.")]
        public string PasswordConfirmation { get; set; }
    }
}
