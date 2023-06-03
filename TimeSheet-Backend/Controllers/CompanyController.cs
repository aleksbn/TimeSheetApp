using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using TimeSheet_Backend.Models.Data;
using TimeSheet_Backend.Models.DTOs;
using TimeSheet_Backend.Warehouse;

namespace TimeSheet_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _unitOfWork.CompanyRepository.GetAll();
                return Ok(_mapper.Map<List<CompanyDTO>>(companies));
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var company = await _unitOfWork.CompanyRepository.Get(c => c.ID == id, new List<string>()
                {
                    "CompanyManager"
                });
                return Ok(_mapper.Map<CompanyDTO>(company));
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCompany([FromBody] CompanyDTO company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Input all required fields in a correct format!");
            }

            try
            {
                await _unitOfWork.CompanyRepository.Insert(_mapper.Map<Company>(company));
                await _unitOfWork.Save();
                return Ok("Company created");
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyDTO editCompany)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Input all required fields in a correct format!");
            }

            try
            {
                var company = await _unitOfWork.CompanyRepository.Get(c => c.ID == editCompany.ID, null);
                company = _mapper.Map<Company>(editCompany);
                _unitOfWork.CompanyRepository.Update(company);
                await _unitOfWork.Save();
                return Ok("Company edited succesfully.");
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCompany(int id, int targetDepartmentId = 0, bool deleteEmployees = false)
        {
            try
            {
                var employees = await _unitOfWork.EmployeeRepository.GetAll(e => e.Department.CompanyID == id);

                if (employees.Count == 0)
                {
                    return NotFound("That company does not exist");
                }

                if (deleteEmployees)
                {
                    foreach (var employee in employees)
                    {
                        var workingTimes = await _unitOfWork.WorkingTimeRepository.GetAll(wt => wt.EmployeeID == employee.ID);
                        _unitOfWork.WorkingTimeRepository.DeleteRange(workingTimes);
                    }
                    _unitOfWork.EmployeeRepository.DeleteRange(employees);
                }
                else
                {
                    if (targetDepartmentId != 0)
                    {
                        var targetDepartment = await _unitOfWork.DepartmentRepository.Get(d => d.ID == targetDepartmentId);
                        if (targetDepartment == null)
                        {
                            return NotFound("That target department does not exist");
                        }
                        foreach (var employee in employees)
                        {
                            employee.DepartmentID = targetDepartmentId;
                            _unitOfWork.EmployeeRepository.Update(employee);
                        }
                    }
                    else
                    {
                        foreach (var employee in employees)
                        {
                            employee.DepartmentID = 0;
                            _unitOfWork.EmployeeRepository.Update(employee);
                        }
                    }
                }
                var departments = await _unitOfWork.DepartmentRepository.GetAll(d => d.CompanyID == id);
                _unitOfWork.DepartmentRepository.DeleteRange(departments);
                await _unitOfWork.CompanyRepository.Delete(id);
                await _unitOfWork.Save();
                return Ok("Company and all selected data deleted.");
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }
    }
}
