using System;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public async static Task<Result> OnBothAsync(this Result result, Func<string, Task<Result>> func, string message)
        {
            await func(message);

            return result;
        }

        public async static Task<T> OnBothAsync<T>(this Result<T> result, Func<Result, Task<T>> func)
        {
            return await func(result);
        }
    }
}
