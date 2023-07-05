using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimeSheet_Backend.Helpers;
using TimeSheet_Backend.Models.Data;
using TimeSheet_Backend.Warehouse;

namespace TimeSheet_Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RandomGenerateController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RandomGenerateController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpPost("{comid}/{numberOfEmployees}/{numberOfDays}")]
        public async Task<IActionResult> GenerateRandoms(int comid, int numberOfEmployees, int numberOfDays)
        {
            string userId = GetUserId();
            var random = new Random();
            var departments = await _unitOfWork.DepartmentRepository.GetAll(d => d.CompanyID == comid);
            var company = await _unitOfWork.CompanyRepository.Get(c => c.ID == comid);

            try
            {
                foreach (var department in departments)
                {
                    for (int i = 0; i < numberOfEmployees; i++)
                    {
                        //generating employee
                        var e = new Employee
                        {
                            ID = "0000000000000",
                            FirstName = ArraysForRandoms.FirstNames[random.Next(0, ArraysForRandoms.FirstNames.Length)],
                            LastName = ArraysForRandoms.LastNames[random.Next(0, ArraysForRandoms.LastNames.Length)],
                            JobTitle = ArraysForRandoms.JobTitles[random.Next(0, ArraysForRandoms.JobTitles.Length)],
                            Degree = ArraysForRandoms.Degrees[random.Next(0, ArraysForRandoms.Degrees.Length)],
                            Address = ArraysForRandoms.Addresses[random.Next(0, ArraysForRandoms.Addresses.Length)] + " " + random.Next(1, 150) + ", " +
                                    ArraysForRandoms.Cities[random.Next(0, ArraysForRandoms.Cities.Length)] + ", Serbia",
                            Phone = "+3816" + random.Next(0, 7) + random.Next(100, 1000) + random.Next(1000, 10000),
                            Email = "temp@temp.net",
                            DateOfBirth = new DateTime(random.Next(DateTime.Now.Year - 75, DateTime.Now.Year - 25), random.Next(1, 13), random.Next(1, 29)),
                            StartOfEmployment = new DateTime(2000, 1, 1),
                            HourlyRate = 0,
                            DepartmentID = department.ID
                        };
                        e.ID = e.DateOfBirth.Day.ToString("00") + e.DateOfBirth.Month.ToString("00") + e.DateOfBirth.Year.ToString().Substring(1, 3) + random.Next(100000, 999999);
                        e.StartOfEmployment = e.DateOfBirth.AddYears(25);
                        var allEmployees = await _unitOfWork.EmployeeRepository.GetAll();
                        var allEmails = allEmployees.Select(e => e.Email);
                        e.Email = e.FirstName.ToLower() + e.LastName.ToLower() + "@" + ArraysForRandoms.EmailProviders[random.Next(0, ArraysForRandoms.EmailProviders.Length)];
                        int counter = 0;
                        while (true)
                        {
                            if(allEmails.Contains(e.Email))
                            {
                                var parts = e.Email.Split('@');
                                parts[0] += counter;
                                counter++;
                                e.Email = parts[0] + "@" + parts[1];
                            }
                            else
                            {
                                break;
                            }
                        }
                        var degreeParts = e.Degree.Split(" ");
                        e.HourlyRate = degreeParts[0] == "PHD" ? random.Next(10, 16) : degreeParts[0] == "Master" ? random.Next(7, 12) : random.Next(5, 10);
                        await _unitOfWork.EmployeeRepository.Insert(e);
                        await _unitOfWork.Save();

                        var emp = await _unitOfWork.EmployeeRepository.Get(em => em.Email == e.Email);
                        string empId = emp.ID;

                        //generating working times
                        var workingTimes = new List<WorkingTime>();
                        var workingDate = DateTime.Now.Date.AddDays(-1);
                        for(int j=numberOfDays; j>0; j--)
                        {
                            while(workingDate.DayOfWeek == DayOfWeek.Sunday || workingDate.DayOfWeek == DayOfWeek.Saturday) 
                            { 
                                workingDate = workingDate.AddDays(-1);
                            }

                            var hoursToAddOrSubstract = random.Next(1, 5);
                            workingTimes.Add(new WorkingTime
                            {
                                Date = workingDate,
                                EmployeeID = emp.ID,
                                StartTime = company.StartTime,
                                EndTime = hoursToAddOrSubstract >= 2 ? 
                                    company.EndTime.Add(new TimeSpan(hoursToAddOrSubstract - 2, 0, 0)) : 
                                    company.EndTime.Subtract(new TimeSpan(hoursToAddOrSubstract - 2, 0, 0))
                            });
                            workingDate = workingDate.AddDays(-1);
                        }
                        await _unitOfWork.WorkingTimeRepository.InsertRange(workingTimes);
                        await _unitOfWork.Save();
                    }
                }
                return Ok("Data generated.");
            }
             catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }
    }
}
