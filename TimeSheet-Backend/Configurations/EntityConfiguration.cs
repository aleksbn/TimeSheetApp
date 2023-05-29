using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeSheetBackend.Models.Data;

namespace TimeSheetBackend.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(
                new Company
                {
                    ID = 1,
                    Name = "Vixtra Corporation",
                    Address = "Street of the Unknown Hero 12",
                    City = "Belgrade",
                    Country = "Serbia",
                    Email = "official@vixtra.com"
                }
                );
        }
    }
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData(
                new Department
                {
                    ID = 1,
                    Name = "Finances",
                    CompanyID = 1
                }, new Department
                {
                    ID = 2,
                    Name = "Manufactoring",
                    CompanyID = 1
                });
        }
    }
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                new Employee
                {
                    ID = "0912986710077",
                    FirstName = "Aleksandar",
                    LastName = "Matic",
                    JobTitle = "Director of Finances",
                    Degree = "Bachelor of Information Technologies",
                    Address = "Dusana Baranina 1/C/10, Bijeljina",
                    Phone = "+38765123789",
                    Email = "aleksbn@gmail.com",
                    DateOfBirth = new DateTime(1986, 12, 9),
                    StartOfEmployment = new DateTime(2012, 10, 11),
                    HourlyRate = 12,
                    CompanyID = 1,
                    DepartmentID = 1
                },
                new Employee
                {
                    ID = "0712992512497",
                    FirstName = "Ivana",
                    LastName = "Rankovic",
                    JobTitle = "Commercialist",
                    Degree = "Graduated Economist",
                    Address = "Nemanjina 110, Belgrade",
                    Phone = "+38160124578",
                    Email = "ivrank@gmail.com",
                    DateOfBirth = new DateTime(1992, 11, 7),
                    StartOfEmployment = new DateTime(2019, 11, 20),
                    HourlyRate = 8,
                    CompanyID = 1,
                    DepartmentID = 1
                },
                new Employee
                {
                    ID = "0505991963258",
                    FirstName = "Jasna",
                    LastName = "Ivkovic",
                    JobTitle = "Accounty",
                    Degree = "Master of Economy",
                    Address = "Ive Andrica 25, Belgrade",
                    Phone = "+38166419526",
                    Email = "jasnaiv@gmail.com",
                    DateOfBirth = new DateTime(1991, 5, 5),
                    StartOfEmployment = new DateTime(2017, 4, 7),
                    HourlyRate = 7,
                    CompanyID = 1,
                    DepartmentID = 1
                },
                new Employee
                {
                    ID = "2210986124578",
                    FirstName = "Mirko",
                    LastName = "Simanic",
                    JobTitle = "Team Leader",
                    Degree = "Master of Information Technologies",
                    Address = "Kirila i Metodija 29, Belgrade",
                    Phone = "+38765142369",
                    Email = "mirko@gmail.com",
                    DateOfBirth = new DateTime(1986, 10, 22),
                    StartOfEmployment = new DateTime(2013, 7, 8),
                    HourlyRate = 12,
                    CompanyID = 1,
                    DepartmentID = 2
                },
                new Employee
                {
                    ID = "0207987124935",
                    FirstName = "Goran",
                    LastName = "Vlacic",
                    JobTitle = "QoS assurance manager",
                    Degree = "Bachelor of Information Technologies",
                    Address = "Janka Veselinovica 20, Belgrade",
                    Phone = "+381621549302",
                    Email = "covla@gmail.com",
                    DateOfBirth = new DateTime(1987, 7, 2),
                    StartOfEmployment = new DateTime(2020, 11, 12),
                    HourlyRate = 10,
                    CompanyID = 1,
                    DepartmentID = 2
                },
                new Employee
                {
                    ID = "0811993988659",
                    FirstName = "Marija",
                    LastName = "Gavranovic",
                    JobTitle = "Software Developer",
                    Degree = "Bachelor of Organisational Sciences",
                    Address = "Laye Kostica 182, Belgrade",
                    Phone = "+381645123698",
                    Email = "makigavra@gmail.com",
                    DateOfBirth = new DateTime(1993, 11, 8),
                    StartOfEmployment = new DateTime(2013, 12, 12),
                    HourlyRate = 10,
                    CompanyID = 1,
                    DepartmentID = 2
                }
                );
        }
    }
    public class WorkingTimeConfiguration : IEntityTypeConfiguration<WorkingTime>
    {
        public void Configure(EntityTypeBuilder<WorkingTime> builder)
        {
            builder.HasData(
                new WorkingTime
                {
                    ID = 1,
                    Date = new DateTime(2023, 4, 3),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0912986710077"
                },
                new WorkingTime
                {
                    ID = 2,
                    Date = new DateTime(2023, 4, 4),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0912986710077"
                },
                new WorkingTime
                {
                    ID = 3,
                    Date = new DateTime(2023, 4, 5),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0912986710077"
                },
                new WorkingTime
                {
                    ID = 4,
                    Date = new DateTime(2023, 4, 6),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0912986710077"
                },
                new WorkingTime
                {
                    ID = 5,
                    Date = new DateTime(2023, 4, 7),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0912986710077"
                },
                new WorkingTime
                {
                    ID = 6,
                    Date = new DateTime(2023, 4, 3),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0712992512497"
                },
                new WorkingTime
                {
                    ID = 7,
                    Date = new DateTime(2023, 4, 4),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0712992512497"
                },
                new WorkingTime
                {
                    ID = 8,
                    Date = new DateTime(2023, 4, 5),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0712992512497"
                },
                new WorkingTime
                {
                    ID = 9,
                    Date = new DateTime(2023, 4, 6),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0712992512497"
                },
                new WorkingTime
                {
                    ID = 10,
                    Date = new DateTime(2023, 4, 7),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0712992512497"
                },
                new WorkingTime
                {
                    ID = 11,
                    Date = new DateTime(2023, 4, 3),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0505991963258"
                },
                new WorkingTime
                {
                    ID = 12,
                    Date = new DateTime(2023, 4, 4),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0505991963258"
                },
                new WorkingTime
                {
                    ID = 13,
                    Date = new DateTime(2023, 4, 5),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0505991963258"
                },
                new WorkingTime
                {
                    ID = 14,
                    Date = new DateTime(2023, 4, 6),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0505991963258"
                },
                new WorkingTime
                {
                    ID = 15,
                    Date = new DateTime(2023, 4, 7),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0505991963258"
                },
                new WorkingTime
                {
                    ID = 16,
                    Date = new DateTime(2023, 4, 3),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "2210986124578"
                },
                new WorkingTime
                {
                    ID = 17,
                    Date = new DateTime(2023, 4, 4),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "2210986124578"
                },
                new WorkingTime
                {
                    ID = 18,
                    Date = new DateTime(2023, 4, 5),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "2210986124578"
                },
                new WorkingTime
                {
                    ID = 19,
                    Date = new DateTime(2023, 4, 6),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "2210986124578"
                },
                new WorkingTime
                {
                    ID = 20,
                    Date = new DateTime(2023, 4, 7),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "2210986124578"
                },
                new WorkingTime
                {
                    ID = 21,
                    Date = new DateTime(2023, 4, 3),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0207987124935"
                },
                new WorkingTime
                {
                    ID = 22,
                    Date = new DateTime(2023, 4, 4),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0207987124935"
                },
                new WorkingTime
                {
                    ID = 23,
                    Date = new DateTime(2023, 4, 5),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0207987124935"
                },
                new WorkingTime
                {
                    ID = 24,
                    Date = new DateTime(2023, 4, 6),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0207987124935"
                },
                new WorkingTime
                {
                    ID = 25,
                    Date = new DateTime(2023, 4, 7),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0207987124935"
                },
                new WorkingTime
                {
                    ID = 26,
                    Date = new DateTime(2023, 4, 3),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0811993988659"
                },
                new WorkingTime
                {
                    ID = 27,
                    Date = new DateTime(2023, 4, 4),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0811993988659"
                },
                new WorkingTime
                {
                    ID = 28,
                    Date = new DateTime(2023, 4, 5),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0811993988659"
                },
                new WorkingTime
                {
                    ID = 29,
                    Date = new DateTime(2023, 4, 6),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0811993988659"
                },
                new WorkingTime
                {
                    ID = 30,
                    Date = new DateTime(2023, 4, 7),
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    EmployeeID = "0811993988659"
                }
                );
        }
    }
}
