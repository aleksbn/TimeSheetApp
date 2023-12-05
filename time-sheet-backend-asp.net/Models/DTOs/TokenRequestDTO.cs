using System.ComponentModel.DataAnnotations;

namespace TimeSheet_Backend.Models.DTOs
{
    public class TokenRequestDTO
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
