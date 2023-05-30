using System.ComponentModel.DataAnnotations;

namespace TimeSheet_Backend.Models.DTOs
{
    public class CreateEmployeeDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "First name must be up to 50 characters long.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(maximumLength: 75, ErrorMessage = "Last name must be up to 75 characters long.")]
        public string LastName { get; set; }
        [Required]
        [StringLength(maximumLength: 75, ErrorMessage = "Job title must be up to 75 characters long.")]
        public string JobTitle { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Degree must be up to 50 characters long.")]
        public string Degree { get; set; }
        [Required]
        [StringLength(maximumLength: 200, ErrorMessage = "Address must be up to 200 characters long.")]
        public string Address { get; set; }
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Phone must be up to 20 characters long.")]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public DateTime StartOfEmployment { get; set; }
        [Required]
        public double HourlyRate { get; set; }
    }

    public class EmployeeDTO: CreateEmployeeDTO
    {
        [StringLength(maximumLength: 13, MinimumLength = 13)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Personal ID must be 13 characters long and containing numbers only.")]
        [Required]
        public string ID { get; set; }

        public virtual IList<WorkingTimeDTO> WorkingTimeDTOs { get; set; }
    }
}
