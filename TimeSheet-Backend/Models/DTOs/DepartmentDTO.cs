using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TimeSheetBackend.Models.Data;

namespace TimeSheetBackend.Models.DTOs
{
    public class CreateDepartmentDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Name od a department must be up to 50 characters long.")]
        public string Name { get; set; }

        [Required]
        public int CompanyID { get; set; }
        public virtual CompanyDTO Company { get; set; }
    }

    public class DepartmentDTO: CreateDepartmentDTO
    {
        [Required]
        public int ID { get; set; }

        public virtual IList<EmployeeDTO> Employees { get; set; }
    }
}
