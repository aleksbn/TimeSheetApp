using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
                return BadRequest(x.Message);
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
                return BadRequest(x.Message);
            }
        }
    }
}
