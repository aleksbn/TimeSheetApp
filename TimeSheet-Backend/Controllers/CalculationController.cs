using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimeSheet_Backend.Models.DTOs;
using TimeSheet_Backend.Warehouse;

namespace TimeSheet_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CalculationController(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpGet("{comid:int}")]
        public async Task<IActionResult> Get(int comid, int year = 0, int month = 0)
        {
            var company = await _unitOfWork.CompanyRepository.Get(c => c.ID == comid);

            if(company == null)
            {
                return BadRequest("That company does not exist.");
            }

            if (company.CompanyManagerId != GetUserId())
            {
                return Unauthorized("That company is not created by this user. You cannot read statistics about it.");
            }

            var employeeList = await _unitOfWork.EmployeeRepository.GetAll(e => e.Department.CompanyID == comid, e => e.OrderBy(em => em.DepartmentID), new List<string>
            {
                "WorkingTimes", 
                "Department"
            });

            List<CalculationDTO> result = new List<CalculationDTO>();

            foreach (var e in employeeList)
            {
                var toRemove = year == 0 && month == 0 ? null :
                        month == 0 ? e.WorkingTimes.Where(wt => wt.Date.Year != year).Select(wt => wt).ToList() :
                        e.WorkingTimes.Where(wt => wt.Date.Year != year || wt.Date.Month != month).Select(wt => wt).ToList();

                if (toRemove != null)
                {
                    foreach (var item in toRemove.ToList())
                    {
                        if(e.WorkingTimes.Contains(item))
                        {
                            e.WorkingTimes.Remove(item);
                        }
                    }
                }

                CalculationDTO calc = new()
                {
                    ID = e.ID,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Department = e.Department.Name,
                    HourlyRate = e.HourlyRate,
                    WorkingDays = 
                        year == 0 && month == 0 ? e.WorkingTimes.Count : 
                        month == 0 ? e.WorkingTimes.Count(wt => wt.Date.Year == year) : 
                        e.WorkingTimes.Count(wt => wt.Date.Year == year && wt.Date.Month == month),
                    RegularWorkingHours = 0,
                    OvertimeHours = 0,
                    Earnings = 0
                };
                int totalRegularHours = 0;
                int overtimeHours = 0;
                foreach (var wt in e.WorkingTimes)
                {
                    totalRegularHours += (int)(wt.EndTime - wt.StartTime).TotalHours <= 8 ? (int)(wt.EndTime - wt.StartTime).TotalHours : (int)(wt.EndTime - wt.StartTime).TotalHours - 8;
                    overtimeHours += (int)(wt.EndTime - wt.StartTime).TotalHours > 8 ? (int)(wt.EndTime - wt.StartTime).TotalHours - 8 : 0;
                }
                calc.RegularWorkingHours = totalRegularHours;
                calc.OvertimeHours = overtimeHours;
                calc.Earnings = overtimeHours * (e.HourlyRate * 2) + calc.RegularWorkingHours * e.HourlyRate;
                result.Add(calc);
            }

            return Ok(result.Where(e => e.WorkingDays > 0).Select(e => e));
        }
    }
}
