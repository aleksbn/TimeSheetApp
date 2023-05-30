using System.ComponentModel.DataAnnotations;

namespace TimeSheet_Backend.Models.DTOs
{
    public class CreateCompanyDTO
    {
        [Required]
        [StringLength(maximumLength: 200, ErrorMessage = "Name must be up to 200 characters long.")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 200, ErrorMessage = "Address must be up to 200 characters long.")]
        public string Address { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "City must be up to 50 characters long.")]
        public string City { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country must be up to 50 characters long.")]
        public string Country { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "You must enter an email address in a correct format.")]
        public string Email { get; set; }
    }

    public class CompanyDTO: CreateCompanyDTO
    {
        [Required]
        public int ID { get; set; }

        public virtual IList<EmployeeDTO> Employees { get; set; }
        public virtual IList<DepartmentDTO> Departments { get; set; }
    }
}
