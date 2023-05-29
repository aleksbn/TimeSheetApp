using AutoMapper;
using TimeSheetBackend.Models.Data;
using TimeSheetBackend.Models.DTOs;

namespace TimeSheetBackend.Configurations
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
        }
    }
}
