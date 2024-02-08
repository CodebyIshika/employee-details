using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqFirstLab
{
    internal class ProjectMethods
    {
        Database database = new Database();

        public void GroupEmployeeByDepartment()
        {
            var groupOfEmployee = database.EmployeeCollection.GroupBy(x => x.DepartmentId);

            foreach(var group in groupOfEmployee )
            {
                Console.WriteLine();

                Console.WriteLine($"Department Id : {group.Key}");

                foreach( var employee in group )
                {
                    Console.WriteLine($"Employee Id: {employee.EmployeeId}, " +
                                      $"Employee Name: {employee.EmployeeFirstName} {employee.EmployeeLastName}");
                }
            }
        }

        public void CalculateSalaryOfDepartments()
        {
            var departmentSalary = database.EmployeeCollection
                                .GroupBy(x => x.DepartmentId)
                                .Select(department => new
                                {
                                    DepartmentId = department.Key,
                                    TotalSalary = department.Sum(e => e.Salary),
                                    AverageSalary = department.Average(e => e.Salary)
                                });

            foreach( var emp in departmentSalary)
            {
                Console.WriteLine($"Average Salary of Department {emp.DepartmentId}: $ {emp.AverageSalary:F2}");
            }

            Console.WriteLine();

            var highestSalaryDepartment = departmentSalary.OrderBy(x => x.TotalSalary).Last();

            Console.WriteLine($"Department {highestSalaryDepartment.DepartmentId} " +
                              $"has the highest total salary of $ {highestSalaryDepartment.TotalSalary:F2}");

        }

        public void GroupEmployeeByProjects()
        {
            var employeeGroup = database.ProjectList
                                .GroupJoin(database.EmployeeCollection,
                                 project => project.DepartmentId,
                                 employee => employee.DepartmentId,
                                 (project, employees) => new
                                 {
                                     projectId = project.ProjectId,
                                     projectName = project.ProjectName,
                                     employees = employees.Select(employee => new
                                     {
                                         employeeId = employee.EmployeeId,
                                         employeeFirstName = employee.EmployeeFirstName,
                                         employeeLastName = employee.EmployeeLastName,
                                     })

                                 });

            foreach (var project in employeeGroup)
            {
                Console.WriteLine($"Project Id: {project.projectId}, Project Name: {project.projectName}");

                foreach (var employee in project.employees)
                {
                    Console.WriteLine($"  Employee Id: {employee.employeeId}, Name: {employee.employeeFirstName} {employee.employeeLastName}");
                }
                Console.WriteLine();
            }
        }

        public void ProjectsByDepartments()
        {
            var projectsCount = database.ProjectList
                .GroupBy(project => project.DepartmentId)
                .Select(department => new
                {
                    DepartmentId = department.Key,
                    TotalProjects = department.Count()
                });

            foreach (var result in projectsCount)
            {
                Console.WriteLine($"Department Id: {result.DepartmentId}, Total Projects: {result.TotalProjects}");
            }
        }

        public void DisplayCompleteInfo()
        {
            var employeeDetails = database.DepartmentList
                                  .GroupJoin(database.EmployeeCollection,
                                   department => department.DepartmentId,
                                   employeeInfo => employeeInfo.DepartmentId,
                                   (department, employees) => new
                                   {
                                       departmentId = department.DepartmentId,
                                       departmentName = department.DepartmentName,
                                       employees = employees.Select(employeeInfo => new
                                       {
                                           employeeId = employeeInfo.EmployeeId,
                                           employeeFirstName = employeeInfo.EmployeeFirstName,
                                           employeeLastName = employeeInfo.EmployeeLastName,
                                           employeeSalary = employeeInfo.Salary,
                                       })   
                                   })
                                   .GroupJoin(database.ProjectList,
                                    department => department.departmentId,
                                    projectInfo => projectInfo.DepartmentId,
                                    (department, projectInfo) => new
                                    {
                                        department,
                                        projects = projectInfo
                                        .Select(x => new
                                        {
                                            projectId = x.ProjectId,
                                            projectName = x.ProjectName,
                                        }).Distinct()
                                    });

            foreach (var info in employeeDetails)
            {
                Console.WriteLine($"Department Id: {info.department.departmentId}, Department Name: {info.department.departmentName}");

                foreach (var employee in info.department.employees)
                {
                    Console.WriteLine($"  Employee Id: {employee.employeeId}, " +
                                      $"Name: {employee.employeeFirstName} {employee.employeeLastName}, " +
                                      $"Salary: ${employee.employeeSalary}, " +
                                      $"Projects: {string.Join(", ", info.projects.Select(p => $"Id: {p.projectId}, Name: {p.projectName}"))}");
                }

                Console.WriteLine();
            }
        }

        public void CheckEmployeeSalary(double Salary)
        {
            var salary = database.EmployeeCollection.Where(x => x.Salary > Salary);
                                       
            Console.WriteLine($"The employee that have salary above the ${Salary}: ");
            foreach (var obj in salary)
            {
                Console.WriteLine($"Employee Name: {obj.EmployeeFirstName} {obj.EmployeeLastName}, Salary: ${obj.Salary}");
            }
        }

        public void DisplaySpecificInfo()
        {
            var employeeProjectDetails = database.EmployeeCollection
                .Join(database.ProjectList,
                    employee => employee.DepartmentId,
                    project => project.DepartmentId,
                    (employee, project) => new
                    {
                        firstName = employee.EmployeeFirstName,
                        lastName = employee.EmployeeLastName,
                        projectName = project.ProjectName
                    })
                .GroupBy(details => new { details.firstName, details.lastName })
                .Select(group => new
                {
                    FirstName = group.Key.firstName,
                    LastName = group.Key.lastName,
                    ProjectNames = string.Join(", ", group.Select(details => details.projectName))
                });

            foreach (var details in employeeProjectDetails)
            {
                Console.WriteLine($"Name: {details.FirstName} {details.LastName}, Projects: {details.ProjectNames}");
            }
        }
    }
}
