using System;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public static Result<TOut> OnSuccess<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TOut>(result.Error);
            }

            return Result.Ok(func(result.Value));
        }

        public static Result<T> OnSuccess<T>(
            this Result<T> result, 
            Action<T> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        public static Result OnSuccess(
            this Result result, 
            Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        public static Result OnSuccess(
            this Result result,
            Func<Result> func)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return func();
        }
    }
}
