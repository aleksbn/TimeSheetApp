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
        public async Task<IActionResult> GetEmployeesFromDepartment(int comId, int depId)
        {
            try
            {
                var employees = await _unitOfWork.EmployeeRepository.GetAll(e => e.Department.CompanyID == comId && e.DepartmentID == depId, null, new List<string>()
                {
                    "WorkingTimes"
                });

                return Ok(_mapper.Map<List<EmployeeDTO>>(employees));
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpGet("{comId:int}")]
        public async Task<IActionResult> GetEmployeesFromCompany(int comId)
        {
            try
            {
                var employees = await _unitOfWork.EmployeeRepository.GetAll(e => e.Department.CompanyID == comId, null, new List<string>()
                {
                    "Department"
                });

                return Ok(_mapper.Map<List<EmployeeDTO>>(employees));
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
        public async Task<IActionResult> PostEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Input all required fields in a correct format!");
            }

            try
            {
                await _unitOfWork.EmployeeRepository.Insert(_mapper.Map<Employee>(employeeDTO));
                await _unitOfWork.Save();
                return Ok("Employee added");
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
    }
}
