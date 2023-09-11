using System;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public static Result OnBoth(this Result result, Action action)
        {
            action();

            return result;
        }

        public static Result OnBoth(this Result result, Action<Result> action)
        {
            action(result);

            return result;
        }

        public static Result OnBoth(this Result result, Func<string, Result> func, string message)
        {
            func(message);

            return result;
        }

        public static Result OnBoth(this Result result, Action<string> func, string message)
        {
            func(message);

            return result;
        }

        public static Result<T> OnBoth<T>(this Result<T> result, Action<T> func)
        {
            func(result.Value);

            return result;
        }

        public static Result<T> OnBoth<T>(this Result<T> result, Func<T> func)
        {
            func();

            return result;
        }
    }
}
