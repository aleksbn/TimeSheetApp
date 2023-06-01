using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{depId}")]
        public async Task<IActionResult> FromDepartment(int depId)
        {
            try
            {
                var workingTimes = await _unitOfWork.WorkingTimeRepository.GetAll(w => w.Employee.DepartmentID == depId, null, new List<string>()
                {
                    "Employee"
                });

                return Ok(_mapper.Map<List<WorkingTimeDTO>>(workingTimes));
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        }

        [HttpGet("{comId}")]
        public async Task<IActionResult> FromCompany(int comId)
        {
            try
            {
                var workingTimes = await _unitOfWork.WorkingTimeRepository.GetAll(w => w.Employee.CompanyID == comId, null, new List<string>()
                {
                    "Employee"
                });

                return Ok(_mapper.Map<List<WorkingTimeDTO>>(workingTimes));
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        }
    }
}
