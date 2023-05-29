using TimeSheetBackend.Models.Data;

namespace TimeSheetBackend.Warehouse
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<Company> CompanyRepository { get; }
        IGenericRepository<Department> DepartmentRepository { get; }
        IGenericRepository<Employee> EmployeeRepository { get; }
        IGenericRepository<WorkingTime> WorkingTimeRepository { get; }
        Task Save();
    }
}
