using System;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public static Result OnBoth(this Result result, Action action)
        {
            action();

            return result;
        }

        public static Result OnBoth(this Task<Result> resultTask, Action action)
        {
            var result = resultTask.Result;

            action();

            return result;
        }

        public static Result OnBoth(this Result result, Action<string> action, string message)
        {
            action(message);

            return result;
        }

        public static Result OnBoth(this Task<Result> resultTask, Action<string> action, string message)
        {
            var result = resultTask.Result;

            action(message);

            return result;
        }

        public static Result<T> OnBoth<T>(this Result<T> result, Action<T> action)
        {
            action(result.Value);

            return result;
        }

        public static Result<T> OnBoth<T>(this Task<Result<T>> resultTask, Action<T> action)
        {
            var result = resultTask.Result;

            action(result.Value);

            return result;
        }

        public static Result<TOut> OnBoth<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func)
        {
            return Result.Map<TOut>(func(result.Value), result);
        }

        public static Result<TOut> OnBoth<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, TOut> func)
        {
            var result = resultTask.Result;

            return Result.Map<TOut>(func(result.Value), result);
        }
    }
}
