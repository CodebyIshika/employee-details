using System;
using System.Collections.Generic;
using System.Data.Common;
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

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine(" --------------------------------------------------------- ");
                    Console.WriteLine("| 1. Display Employee Groups accoring to their department.");
                    Console.WriteLine(" ----------------------------------------------------------");
                    Console.WriteLine("| 2. Display Salary of each department.");
                    Console.WriteLine(" ----------------------------------------------------------");
                    Console.WriteLine("| 3. Display Employee Groups accoring to their projects.");
                    Console.WriteLine(" ----------------------------------------------------------");
                    Console.WriteLine("| 4. Display total number of projects in each department.");
                    Console.WriteLine(" -----------------------------------------------------------");
                    Console.WriteLine("| 5. Display complete information of employee's.");
                    Console.WriteLine(" ----------------------------------------------------------");
                    Console.WriteLine("| 6. Display Employee's that have salary greater than 50000.");
                    Console.WriteLine(" ----------------------------------------------------------");
                    Console.WriteLine("| 7. Display employee name's and their projects.");
                    Console.WriteLine(" ----------------------------------------------------------");
                    Console.WriteLine("| 8. Exit");
                    Console.WriteLine(" ----------------------------------------------------------");
                    Console.WriteLine();
                    Console.Write("Enter your choice (1-8): ");
                    int userChoice = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    switch (userChoice)
                    {
                        case 1:
                            Console.Clear();
                            projectMethodsObj.GroupEmployeeByDepartment();
                            break;

                        case 2:
                            Console.Clear();
                            projectMethodsObj.CalculateSalaryOfDepartments();
                            break;

                        case 3:
                            Console.Clear();
                            projectMethodsObj.GroupEmployeeByProjects();
                            break;

                        case 4:
                            Console.Clear();
                            projectMethodsObj.ProjectsByDepartments();
                            break;

                        case 5:
                            Console.Clear();
                            projectMethodsObj.DisplayCompleteInfo();
                            break;

                        case 6:
                            Console.Clear();
                            projectMethodsObj.CheckEmployeeSalary(50000);
                            break;

                        case 7:
                            Console.Clear();
                            projectMethodsObj.DisplaySpecificInfo();
                            break;

                        case 8:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Please enter a valid choice.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

        }
    }
}
