using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{comId}/{depId}")]
        public async Task<IActionResult> GetEmployeesFromDepartment(int comId, int depId, [FromQuery] RequestParams requestParams)
        {
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
