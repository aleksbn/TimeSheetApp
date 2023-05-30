using AutoMapper;
using TimeSheet_Backend.Models.Data;
using TimeSheet_Backend.Models.DTOs;

namespace TimeSheet_Backend.Configurations
{
    public class MapperInitializer: Profile
    {
        public MapperInitializer()
        {
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<Company, CreateCompanyDTO>().ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<Department, CreateDepartmentDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee, CreateEmployeeDTO>().ReverseMap();
            CreateMap<WorkingTime, WorkingTimeDTO>().ReverseMap();
            CreateMap<WorkingTime, CreateWorkingTimeDTO>().ReverseMap();
            CreateMap<AppUser, RegisterUserDTO>().ReverseMap();
            CreateMap<AppUser, LoginUserDTO>().ReverseMap();
        }
    }
}
