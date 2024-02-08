using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqFirstLab
{
    internal class Database
    {
        public List<Employee> EmployeeCollection = new List<Employee>()
        {
            new Employee() { EmployeeId = 101, EmployeeFirstName = "James", EmployeeLastName = "Ford", DepartmentId = 11, Salary = 40000},
            new Employee() { EmployeeId = 102, EmployeeFirstName = "Aaron", EmployeeLastName = "BlackFord", DepartmentId = 12, Salary = 50000},
            new Employee() { EmployeeId = 103, EmployeeFirstName = "Nick", EmployeeLastName = "Jonas", DepartmentId = 13, Salary = 40000},
            new Employee() { EmployeeId = 104, EmployeeFirstName = "Alex", EmployeeLastName = "Volkov", DepartmentId = 11, Salary = 80000},
            new Employee() { EmployeeId = 105, EmployeeFirstName = "Ryan", EmployeeLastName = "Hawkins", DepartmentId = 13, Salary = 30000},
            new Employee() { EmployeeId = 106, EmployeeFirstName = "Joey", EmployeeLastName = "Anderson", DepartmentId = 12, Salary = 60000},
            new Employee() { EmployeeId = 107, EmployeeFirstName = "Monica", EmployeeLastName = "Smith", DepartmentId = 13, Salary = 60000},
            new Employee() { EmployeeId = 108, EmployeeFirstName = "Ava", EmployeeLastName = "Clark", DepartmentId = 11, Salary = 70000},
            new Employee() { EmployeeId = 109, EmployeeFirstName = "Zahra", EmployeeLastName = "Wilson", DepartmentId = 13, Salary = 20000},
            new Employee() { EmployeeId = 110, EmployeeFirstName = "Rowan", EmployeeLastName = "Brown", DepartmentId = 12, Salary = 50000},
        };

        public List<Department> DepartmentList = new List<Department>()
        {
            new Department() { DepartmentId = 11, DepartmentName = "IT" },
            new Department() { DepartmentId = 12, DepartmentName = "Finance" },
            new Department() { DepartmentId = 13, DepartmentName = "Sales" },
        };

        public List<Project> ProjectList = new List<Project>()
        { 
            new Project() { DepartmentId = 11, ProjectId = 1, ProjectName = "Data Security"},
            new Project() { DepartmentId = 11, ProjectId = 2, ProjectName = "Artificial Intelligence Integration"},
            new Project() { DepartmentId = 12, ProjectId = 3, ProjectName = "Financial Analysis"},
            new Project() { DepartmentId = 13, ProjectId = 4, ProjectName = "Sales Growth Initiative"},
            new Project() { DepartmentId = 13, ProjectId = 5, ProjectName = "Market Expansion Strategy"},
        };
    }
}
