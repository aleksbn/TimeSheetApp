using TimeSheet_Backend.Models.Data;

namespace TimeSheet_Backend.Warehouse
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Company> CompanyRepository { get; }
        IGenericRepository<Department> DepartmentRepository { get; }
        IGenericRepository<Employee> EmployeeRepository { get; }
        IGenericRepository<WorkingTime> WorkingTimeRepository { get; }
        Task Save();
    }
}
