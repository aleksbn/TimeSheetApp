namespace TimeSheet_Backend.Models.DTOs
{
    public class CalculationDTO
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public double HourlyRate { get; set; }
        public int WorkingDays { get; set; }
        public int RegularWorkingHours { get; set; }
        public int OvertimeHours { get; set;}
        public double Earnings { get; set; }
    }
}
