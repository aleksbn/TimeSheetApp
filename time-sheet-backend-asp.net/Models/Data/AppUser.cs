using Microsoft.AspNetCore.Identity;

namespace TimeSheet_Backend.Models.Data
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual IList<Company> Companies { get; set; }
    }
}
