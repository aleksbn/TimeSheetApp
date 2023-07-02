using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimeSheet_Backend.Models.Data;
using TimeSheet_Backend.Models.DTOs;
using TimeSheet_Backend.Warehouse;

namespace TimeSheet_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{comId}/{depId}")]
        public async Task<IActionResult> GetEmployeesFromDepartment(int comId, int depId, [FromQuery] RequestParams requestParams)
        {
            var com = await _unitOfWork.CompanyRepository.Get(c => c.ID == comId);
            if ( com == null || com.CompanyManagerId != GetUserId()) 
            {
                return Unauthorized("That company is not created by this user. You cannot read its data.");
            }
            try
            {
                var employees = await _unitOfWork.EmployeeRepository.GetAll(e => e.Department.CompanyID == comId && e.DepartmentID == depId, e => e.OrderBy(em => em.ID), new List<string>()
                {
                    "WorkingTimes", "Department"
                });
                var toReturn = _mapper.Map<List<EmployeeDTO>>(employees.Skip(requestParams.PageNumber * requestParams.PageSize).Take(requestParams.PageSize));
                return Ok(toReturn);
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpGet("{comId:int}")]
        public async Task<IActionResult> GetEmployeesFromCompany(int comId, [FromQuery] RequestParams requestParams)
        {
            var com = await _unitOfWork.CompanyRepository.Get(c => c.ID == comId);
            if (com == null || com.CompanyManagerId != GetUserId())
            {
                return Unauthorized("That company is not created by this user. You cannot read its data.");
            }
            try
            {
                var employees = await _unitOfWork.EmployeeRepository.GetAll(e => e.Department.CompanyID == comId, e => e.OrderBy(em => em.DepartmentID), new List<string>()
                {
                    "Department"
                });

                var toReturn = _mapper.Map<List<EmployeeDTO>>(employees.Skip(requestParams.PageNumber * requestParams.PageSize).Take(requestParams.PageSize));
                return Ok(toReturn);
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpGet("{empId}")]
        public async Task<IActionResult> GetEmployee(string empId)
        {
            try
            {
                var employee = await _unitOfWork.EmployeeRepository.Get(e => e.ID == empId, new List<string>()
                {
                    "Department",
                    "WorkingTimes"
                });

                var com = await _unitOfWork.CompanyRepository.Get(c => c.ID == employee.Department.CompanyID);
                if (com == null || com.CompanyManagerId != GetUserId())
                {
                    return Unauthorized("That company is not created by this user. You cannot read its data.");
                }

                return Ok(_mapper.Map<EmployeeDTO>(employee));
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] CreateEmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Input all required fields in a correct format!");
            }

            try
            {
                var dep = await _unitOfWork.DepartmentRepository.Get(d => d.ID == employeeDTO.DepartmentId);
                var com = await _unitOfWork.CompanyRepository.Get(c => c.ID == dep.CompanyID);
                if (com == null || com.CompanyManagerId != GetUserId())
                {
                    return Unauthorized("That company is not created by this user. You cannot add to its data.");
                }
                await _unitOfWork.EmployeeRepository.Insert(_mapper.Map<Employee>(employeeDTO));
                await _unitOfWork.Save();
                return Ok("Employee created");
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Input all required fields in a correct format!");
            }

            try
            {
                var dep = await _unitOfWork.DepartmentRepository.Get(d => d.ID == employeeDTO.DepartmentId);
                var com = await _unitOfWork.CompanyRepository.Get(c => c.ID == dep.CompanyID);
                if (com == null || com.CompanyManagerId != GetUserId())
                {
                    return Unauthorized("That company is not created by this user. You cannot edit its data.");
                }

                var employee = await _unitOfWork.EmployeeRepository.Get(e => e.ID == employeeDTO.ID);
                employee = _mapper.Map<Employee>(employeeDTO);
                _unitOfWork.EmployeeRepository.Update(employee);
                await _unitOfWork.Save();
                return Ok("Employee edited succesfully.");
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var employee = await _unitOfWork.EmployeeRepository.Get(e => e.ID == id);
            if (employee != null)
            {
                var dep = await _unitOfWork.DepartmentRepository.Get(d => d.ID == employeeDTO.DepartmentId);
                var com = await _unitOfWork.CompanyRepository.Get(c => c.ID == dep.CompanyID);
                if (com == null || com.CompanyManagerId != GetUserId())
                {
                    return Unauthorized("That company is not created by this user. You cannot delete its data.");
                }

                await _unitOfWork.EmployeeRepository.DeleteByString(id);
                await _unitOfWork.Save();
                var workingTimes = await _unitOfWork.WorkingTimeRepository.GetAll(wt => wt.EmployeeID == id);
                _unitOfWork.WorkingTimeRepository.DeleteRange(workingTimes);
                await _unitOfWork.Save();
                return Ok("Employee deleted");
            }
            else
            {
                return NotFound("That employee does not exist");
            }
        }
    }
}
