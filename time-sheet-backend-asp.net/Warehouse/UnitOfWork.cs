using TimeSheet_Backend.Models.Data;

namespace TimeSheet_Backend.Warehouse
{
    public class UnitOfWork : IUnitOfWork
    {
        public DatabaseContext _context { get; }

        private IGenericRepository<Company> _companies;
        private IGenericRepository<Employee> _employees;
        private IGenericRepository<Department> _departments;
        private IGenericRepository<WorkingTime> _workingTimes;
        
        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IGenericRepository<Company> CompanyRepository => _companies ??= new GenericRepository<Company>(_context);
        public IGenericRepository<Department> DepartmentRepository => _departments ??= new GenericRepository<Department>(_context);
        public IGenericRepository<Employee> EmployeeRepository => _employees ??= new GenericRepository<Employee>(_context);
        public IGenericRepository<WorkingTime> WorkingTimeRepository => _workingTimes ??= new GenericRepository<WorkingTime>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
