using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
                return BadRequest(x.Message);
            }
        }

        [HttpGet("{comId}/{depId}")]
        public async Task<IActionResult> GetAllFromDeparment(int comId, int depId)
        {
            try
            {
                var departments = await _unitOfWork.DepartmentRepository.GetAll(d => d.CompanyID == comId && d.ID == depId, null, new List<string>()
                {
                    "Employees"
                });
                return Ok(_mapper.Map<List<DepartmentDTO>>(departments));
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        }
    }
}
