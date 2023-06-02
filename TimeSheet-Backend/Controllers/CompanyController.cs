using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    }
}
