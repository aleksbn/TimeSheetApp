using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheetBackend.Models.Data
{
    public class WorkingTime
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }

        [ForeignKey(nameof(Employee))]
        [Required]
        public string EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
