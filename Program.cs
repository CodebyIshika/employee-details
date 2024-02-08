using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqFirstLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProjectMethods projectMethodsObj = new ProjectMethods();
            projectMethodsObj.GroupEmployeeByDepartment();
            Console.WriteLine();

            projectMethodsObj.CalculateSalaryOfDepartments();
            Console.WriteLine();

            projectMethodsObj.GroupEmployeeByProjects();
            Console.WriteLine();

            projectMethodsObj.ProjectsByDepartments();
            Console.WriteLine();

            projectMethodsObj.DisplayCompleteInfo();
            Console.WriteLine();

            projectMethodsObj.CheckEmployeeSalary(50000);
            Console.WriteLine();

            projectMethodsObj.DisplaySpecificInfo();
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
