namespace RailwayExtensions.Console.App
{
    public class SalaryRepository : ISalaryRepository
    {
        public decimal GetSalaryById(int id)
        {
            return id * 1000;
        }
    }
}
