using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet_Backend.Models.Data
{
    public class RefreshToken
    {
        [Key]
        public int ID { get; set; }

        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateExpired { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(AppUser))]
        public AppUser User { get; set; }
    }
}
