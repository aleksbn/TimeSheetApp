using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TimeSheet_Backend.Models.Data;
using TimeSheet_Backend.Models.DTOs;
using TimeSheet_Backend.Warehouse;

namespace TimeSheet_Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkingTimeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkingTimeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkingTime(int id)
        {
            try
            {
                var workingTime = await _unitOfWork.WorkingTimeRepository.Get(wt => wt.ID == id, new List<string>
                {
                    "Employee"
                });
                var toReturn = _mapper.Map<WorkingTimeDTO>(workingTime);
                return Ok(toReturn);
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);

            }
        }
        
        [HttpGet("{depId}")]
        public async Task<IActionResult> FromDepartment(int depId, [FromQuery] RequestParams requestParams)
        {
            try
            {
                var workingTimes = await _unitOfWork.WorkingTimeRepository.GetAll(
                    w => w.Employee.DepartmentID == depId, 
                    w => w.OrderBy(wt => wt.Employee.FirstName).ThenBy(wt => wt.Employee.LastName).ThenBy(wt => wt.Date), 
                    new List<string>()
                {
                    "Employee"
                });
                var toReturn = _mapper.Map<List<WorkingTimeDTO>>(workingTimes.Skip(requestParams.PageNumber * requestParams.PageSize).Take(requestParams.PageSize));
                return Ok( new { toReturn, workingTimes.Count  });
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpGet("{empId}")]
        public async Task<IActionResult> FromEmployee(string empId, [FromQuery] RequestParams requestParams)
        {
            try
            {
                var workingTimes = await _unitOfWork.WorkingTimeRepository.GetAll(
                    w => w.Employee.ID == empId,
                    w => w.OrderBy(wt => wt.Employee.FirstName).ThenBy(wt => wt.Employee.LastName).ThenBy(wt => wt.Date),
                    new List<string>()
                {
                    "Employee"
                });
                var toReturn = _mapper.Map<List<WorkingTimeDTO>>(workingTimes.Skip(requestParams.PageNumber * requestParams.PageSize).Take(requestParams.PageSize));
                return Ok(new { toReturn, workingTimes.Count });
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpGet("{comId}")]
        public async Task<IActionResult> FromCompany(int comId, [FromQuery]RequestParams requestParams)
        {
            try
            {
                var workingTimes = await _unitOfWork.WorkingTimeRepository.GetAll(
                    w => w.Employee.Department.CompanyID == comId, w => 
                    w.OrderBy(wt => wt.Employee.FirstName).ThenBy(wt => wt.Employee.LastName).ThenBy(wt => wt.Date), 
                    new List<string>()
                {
                    "Employee"
                });
                var toReturn = _mapper.Map<List<WorkingTimeDTO>>(workingTimes.Skip(requestParams.PageNumber * requestParams.PageSize).Take(requestParams.PageSize));
                return Ok(new { toReturn, workingTimes.Count });
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpPost]
        [ActionName("create")]
        public async Task<IActionResult> PostWorkingTime([FromBody] CreateWorkingTimeDTO workingTimeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Input all required fields in a correct format!");
            }

            try
            {
                await _unitOfWork.WorkingTimeRepository.Insert(_mapper.Map<WorkingTime>(workingTimeDTO));
                await _unitOfWork.Save();
                return Ok("Working time added");
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpPost]
        [ActionName("createRangeForCompany")]
        public async Task<IActionResult> PostWorkingTimeRange(int comId, string date = null, string start = null, string end = null)
        {
            try
            {
                List<WorkingTime> times = new();
                var allEmployees = await _unitOfWork.EmployeeRepository.GetAll(e => e.Department.CompanyID == comId);
                var company = await _unitOfWork.CompanyRepository.Get(c => c.ID == comId);
                List<Employee> employees = new(allEmployees);
                List<WorkingTime> workingTimes = new();
                DateTime currentDate = DateTime.Today;
                TimeSpan? startTime = company.StartTime == null ? company.StartTime : new TimeSpan(8,0,0);
                TimeSpan? endTime = company.EndTime == null ? company.EndTime : new TimeSpan(8, 0, 0);

                if (date != null)
                {
                    currentDate = DateTime.Parse(date);
                }
                if(start != null)
                {
                    startTime = TimeSpan.Parse(start);
                }
                if (end != null)
                {
                    endTime = TimeSpan.Parse(end);
                }

                List<WorkingTime> allWorkingTimess = new(await _unitOfWork.WorkingTimeRepository.GetAll(wt => wt.Employee.Department.CompanyID == comId));

                if (allWorkingTimess.Where(wt => wt.Date.ToLongDateString() == currentDate.ToLongDateString()).Select(wt => wt).Any())
                {
                    return BadRequest("You already added some employee's working times on that date.");
                }

                foreach (var employee in employees)
                {
                    workingTimes.Add(new WorkingTime()
                    {
                        Date = currentDate,
                        StartTime = startTime.Value,
                        EndTime = endTime.Value,
                        EmployeeID = employee.ID
                    });
                }
                await _unitOfWork.WorkingTimeRepository.InsertRange(workingTimes);
                await _unitOfWork.Save();
                return Ok("Working times added");
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditWorkingTime([FromBody] WorkingTimeDTO workingTimeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Input all required fields in a correct format!");
            }

            try
            {
                var workingTime = await _unitOfWork.WorkingTimeRepository.Get(wt => wt.ID == workingTimeDTO.ID);
                workingTime = _mapper.Map<WorkingTime>(workingTimeDTO);
                _unitOfWork.WorkingTimeRepository.Update(workingTime);
                await _unitOfWork.Save();
                return Ok("Working time edited succesfully.");
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [ActionName("deleteWorkingTime")]
        public async Task<IActionResult> DeleteSingleWorkingTime(int id)
        {
            var workingTime = await _unitOfWork.WorkingTimeRepository.Get(wt => wt.ID == id);
            if (workingTime != null)
            {
                await _unitOfWork.WorkingTimeRepository.Delete(id);
                await _unitOfWork.Save();
                return Ok("Working time deleted");
            }
            else
            {
                return NotFound("That working time does not exist");
            }
        }
    }
}
