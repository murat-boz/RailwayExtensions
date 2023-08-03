using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public async static Task<Result<TOut>> MapAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<TOut>> func)
        {
            if (result.IsFailure)
            {
                return Result.Failure<TOut>(result.Error);
            }

            return Result.Ok(await func(result.Value));
        }
    }
}
