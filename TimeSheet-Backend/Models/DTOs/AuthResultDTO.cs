namespace TimeSheet_Backend.Models.DTOs
{
    public class AuthResultDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
