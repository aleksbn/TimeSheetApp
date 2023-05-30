using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TimeSheet_Backend.Models.Data;

namespace TimeSheet_Backend.Models.Data
{
    public class Company
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [Required]
        public string Email { get; set; }
        [AllowNull]
        [ForeignKey(nameof(AppUser))]
        public string CompanyManagerId { get; set; }

        public virtual AppUser CompanyManager { get; set; }
        public virtual IList<Employee> Employees { get; set; }
        public virtual IList<Department> Departments { get; set; }
    }
}
