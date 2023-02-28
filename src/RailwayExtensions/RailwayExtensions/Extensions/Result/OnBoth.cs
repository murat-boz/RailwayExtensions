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

        public static T OnBoth<T>(this Result<T> result, Func<Result, T> func)
        {
            return func(result);
        }
    }
}
