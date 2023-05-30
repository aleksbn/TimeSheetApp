using System.ComponentModel.DataAnnotations;

namespace TimeSheet_Backend.Models.DTOs
{
    public class CreateWorkingTimeDTO
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public string EmployeeID { get; set; }
        public virtual EmployeeDTO Employee { get; set; }
    }

    public class WorkingTimeDTO: CreateWorkingTimeDTO
    {
        [Required]
        public int ID { get; set; }
    }
}
