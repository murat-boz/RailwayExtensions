namespace RailwayExtensions.Console.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            ISalaryRepository salaryRepository     = new SalaryRepository();

            var employee = employeeRepository.GetEmployeeById(25);
            var salary   = salaryRepository.GetSalaryById(25);

            if (salary <= 25000)
            {
                salary += 5000;
            }

            employee.Salary = salary;
        }
    }
}
