namespace RailwayExtensions.Console.App
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Employee GetEmployeeById(int id)
        {
            if (id >= 25 && id <= 45)
            {
                throw new System.InvalidOperationException();
            }

            return new Employee
            {
                Id      = id,
                Name    = $"Name {id}",
                Surname = $"Surname {id}"
            };
        }
    }
}
