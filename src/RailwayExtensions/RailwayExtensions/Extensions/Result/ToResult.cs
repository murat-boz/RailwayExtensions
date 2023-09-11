using System.Threading.Tasks;
using System;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public static Result<T> ToResult<T>(
            this T dataToBeCreated)
        {
            return Result.Ok(dataToBeCreated);
        }

        public async static Task<Result<T>> ToResultAsync<T>(
            this T dataToBeCreated)
        {
            return Result.Ok(dataToBeCreated);
        }
    }
}
