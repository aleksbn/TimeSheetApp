using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimeSheet_Backend.Models.Data;
using TimeSheet_Backend.Models.DTOs;
using TimeSheet_Backend.Warehouse;

namespace TimeSheet_Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public CompanyController(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _unitOfWork.CompanyRepository.GetAll();
                return Ok(_mapper.Map<List<CompanyDTO>>(companies).Where(c => c.CompanyManagerId == GetUserId()));
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
                return company.CompanyManagerId == GetUserId() ? Ok(_mapper.Map<CompanyDTO>(company)) : Unauthorized("That company is not created by this user. You cannot read it.");
            }
            catch (Exception x)
            {
                return BadRequest(x.InnerException.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCompany([FromBody] CreateCompanyDTO company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Input all required fields in a correct format!");
            }

            try
            {
                var toInsert = _mapper.Map<Company>(company);
                toInsert.CompanyManagerId = GetUserId();
                await _unitOfWork.CompanyRepository.Insert(toInsert);
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
                if(editCompany.CompanyManagerId != GetUserId())
                {
                    return Unauthorized("That company is not created by this user. You cannot edit it.");
                }
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCompany(int id, int targetDepartmentId = 0, bool deleteEmployees = false )
        {
            var companyExists = await _unitOfWork.CompanyRepository.Get(c => c.ID == id);
            if(companyExists.CompanyManagerId == GetUserId())
            {
                return Unauthorized("That company is not created by this user. You cannot delete it.");
            }

            try
            {
                var employees = await _unitOfWork.EmployeeRepository.GetAll(e => e.Department.CompanyID == id);

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
                await _unitOfWork.Save();
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
