using System;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultAsyncExtensions
    {
        public static async Task<Result> BindAsync<TIn>(
            this Result<TIn> result,
            Func<TIn, Task<Result>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail(result.Error);
            }

            return await func(result.Value);
        }

        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<Result<TOut>>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TOut>(result.Error);
            }

            return await func(result.Value);
        }
    }
}
