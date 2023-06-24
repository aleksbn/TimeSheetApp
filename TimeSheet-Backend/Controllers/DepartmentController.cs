using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeSheet_Backend.Models.Data;
using TimeSheet_Backend.Models.DTOs;
using TimeSheet_Backend.Warehouse;

namespace TimeSheet_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAll(int id)
        {
            try
            {
                var departments = await _unitOfWork.DepartmentRepository.GetAll(d => d.CompanyID == id, null, new List<string>()
                {
                    "Employees"
                });
                return Ok(_mapper.Map<List<DepartmentDTO>>(departments));
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpGet("{comId}/{depId}")]
        public async Task<IActionResult> Get(int comId, int depId)
        {
            try
            {
                var department = await _unitOfWork.DepartmentRepository.Get(d => d.CompanyID == comId && d.ID == depId, new List<string>()
                {
                    "Employees"
                });
                return Ok(_mapper.Map<DepartmentDTO>(department));
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostDepartment([FromBody] CreateDepartmentDTO departmentDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Input all required fields in a correct format!");
            }

            try
            {
                await _unitOfWork.DepartmentRepository.Insert(_mapper.Map<Department>(departmentDTO));
                await _unitOfWork.Save();
                return Ok("Department created");
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditDepartment([FromBody] DepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Input all required fields in a correct format!");
            }

            try
            {
                var department = await _unitOfWork.DepartmentRepository.Get(d => d.ID == departmentDTO.ID);
                department = _mapper.Map<Department>(departmentDTO);
                _unitOfWork.DepartmentRepository.Update(department);
                await _unitOfWork.Save();
                return Ok("Department edited succesfully.");
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDepartment(int id, bool deleteEmployees = false, int targetDepId = 0)
        {
            try
            {
                var deletionDepartment = await _unitOfWork.DepartmentRepository.Get(d => d.ID == id);

                if (deletionDepartment == null)
                {
                    return NotFound("That department for deletion does not exist");
                }
                if (deleteEmployees)
                {
                    var employees = await _unitOfWork.EmployeeRepository.GetAll(e => e.DepartmentID == id);
                    foreach (var employee in employees)
                    {
                        var workingTimes = await _unitOfWork.WorkingTimeRepository.GetAll(wt => wt.EmployeeID == employee.ID);
                        _unitOfWork.WorkingTimeRepository.DeleteRange(workingTimes);
                    }
                    _unitOfWork.EmployeeRepository.DeleteRange(employees);
                }
                else
                {
                    if (targetDepId != 0)
                    {
                        var targetDepartment = await _unitOfWork.DepartmentRepository.Get(d => d.ID == targetDepId);
                        if (targetDepartment == null)
                        {
                            return NotFound("That target department does not exist");
                        }
                        else
                        {
                            var employees = await _unitOfWork.EmployeeRepository.GetAll(e => e.DepartmentID == id);
                            foreach (var employee in employees)
                            {
                                employee.DepartmentID = targetDepId;
                                _unitOfWork.EmployeeRepository.Update(employee);
                            }
                        }
                    }
                    else
                    {
                        var employees = await _unitOfWork.EmployeeRepository.GetAll(e => e.DepartmentID == id);
                        foreach (var employee in employees)
                        {
                            employee.DepartmentID = 0;
                            _unitOfWork.EmployeeRepository.Update(employee);
                        }
                    }
                }
                await _unitOfWork.Save();
                await _unitOfWork.DepartmentRepository.Delete(deletionDepartment.ID);
                await _unitOfWork.Save();
                return Ok("Department deleted");
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }
    }
}
