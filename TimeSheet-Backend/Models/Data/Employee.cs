using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet_Backend.Models.Data
{
    public class Employee
    {
        [Key]
        [StringLength(13)]
        [Required]
        public string ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public DateTime StartOfEmployment { get; set; }
        [Required]
        public double HourlyRate { get; set; }

        [ForeignKey(nameof(Department))]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyID { get; set; }
        public virtual Company Company { get; set; }

        public virtual IList<WorkingTime> WorkingTimes { get; set; }
    }
}
