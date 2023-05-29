using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TimeSheetBackend.Models.Data
{
    public class Department
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Company))]
        [Required]
        public int CompanyID { get; set; }
        public virtual Company Company { get; set; }

        public virtual IList<Employee> Employees { get; set; }
    }
}
