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

        /// <summary>
        /// The method aims to group employees based on their department ID.
        /// It uses groupby to group the employee collection by the DepartmentId property. 
        /// </summary>
        public void GroupEmployeeByDepartment()
        {
            Console.Clear();
            var groupOfEmployee = database.EmployeeCollection.GroupBy(x => x.DepartmentId);

            Console.WriteLine();
            foreach (var group in groupOfEmployee )
            {

                Console.WriteLine($"Department Id : {group.Key}");

                foreach ( var employee in group )
                {
                    Console.WriteLine($"Employee Id: {employee.EmployeeId}, " +
                                      $"Employee Name: {employee.EmployeeFirstName} {employee.EmployeeLastName}");
                }
                Console.WriteLine();
            }

        }


        /// <summary>
        /// This method checks employee salary by their department. It displays the average salary for 
        /// each department, along with the department's highest total salary. It uses Sum and Average to 
        /// compute the total and average salary, respectively, for each department.
        /// </summary>
        public void CalculateSalaryOfDepartments()
        {
            Console.Clear();

            var departmentSalary = database.EmployeeCollection
                                .GroupBy(x => x.DepartmentId)
                                .Select(department => new
                                {
                                    DepartmentId = department.Key,
                                    TotalSalary = department.Sum(e => e.Salary),
                                    AverageSalary = department.Average(e => e.Salary)
                                });

            Console.WriteLine();
            foreach( var emp in departmentSalary)
            {
                Console.WriteLine($"Average Salary of Department {emp.DepartmentId}: $ {emp.AverageSalary:F2}");
            }

            Console.WriteLine();

            var highestSalaryDepartment = departmentSalary.OrderBy(x => x.TotalSalary).Last();

            Console.WriteLine($"Department {highestSalaryDepartment.DepartmentId} " +
                              $"has the highest total salary of $ {highestSalaryDepartment.TotalSalary:F2}");

        }

        /// <summary>
        /// This method organize employees based on the projects they are involved in. 
        /// This performs a group join between the ProjectList and EmployeeCollection based on the common department ID.
        /// </summary>
        public void GroupEmployeeByProjects()
        {
            Console.Clear();

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

            Console.WriteLine();
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


        /// <summary>
        /// This method group the ProjectList by the DepartmentId. Then, it calculates the total number of 
        /// projects in each department
        /// </summary>
        public void ProjectsByDepartments()
        {
            Console.Clear();

            var projectsCount = database.ProjectList
                .GroupBy(project => project.DepartmentId)
                .Select(department => new
                {
                    DepartmentId = department.Key,
                    TotalProjects = department.Count()
                });

            Console.WriteLine();

            foreach (var result in projectsCount)
            {
                Console.WriteLine();
                Console.WriteLine($"Department Id: {result.DepartmentId}, Total Projects: {result.TotalProjects}");
            }
        }


        /// <summary>
        /// This display information about employees, including their department, salary, and associated projects.
        /// It performs a group join between the DepartmentList and EmployeeCollection based on the common department ID. 
        /// Then, it then performs another group join with the ProjectList to include details about the projects 
        /// associated with each department. The method iterates through the results and prints the department ID, name, 
        /// and details of each employee, including their ID, full name, salary, and a list of associated projects with 
        /// their IDs and names.
        /// </summary>
        public void DisplayCompleteInfo()
        {
            Console.Clear();

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

            Console.WriteLine();

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


        /// <summary>
        /// This method filters employees based on a specified salary threshold, and then displays 
        /// the employees who have salaries exceeding the given threshold. 
        /// </summary>
        /// <param name="Salary"></param>
        public void CheckEmployeeSalary(double Salary)
        {
            Console.Clear();
            var salary = database.EmployeeCollection.Where(x => x.Salary > Salary);

            Console.WriteLine();

            Console.WriteLine($"The employee that have salary above the ${Salary}: ");
            foreach (var obj in salary)
            {
                Console.WriteLine($"Employee Name: {obj.EmployeeFirstName} {obj.EmployeeLastName}, Salary: ${obj.Salary}");
            }
        }

        /// <summary>
        /// This method joins the EmployeeCollection and ProjectList based on their shared DepartmentId.
        /// The results are grouped by the key of an employee's first and last name. For each group, 
        /// the method selects and formats relevant information, such as the employee's full name and 
        /// a concise list of project names they are associated with. 
        /// </summary>
        public void DisplaySpecificInfo()
        {
            Console.Clear();

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

            Console.WriteLine();
            foreach (var details in employeeProjectDetails)
            {
                Console.WriteLine($"Name: {details.FirstName} {details.LastName}, Projects: {details.ProjectNames}");
            }
        }
    }
}
