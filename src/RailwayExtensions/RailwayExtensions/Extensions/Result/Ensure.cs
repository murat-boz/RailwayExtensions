using System;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public static Result<T> Ensure<T>(
            this Result<T> result, 
            Func<T, bool> predicate, 
            string errorMessage)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return predicate(result.Value) 
                ? result 
                : Result.Failure<T>(errorMessage);
        }

        public static Result Ensure(
            this Result result,
            Func<bool> predicate,
            string errorMessage)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return predicate()
                ? result
                : Result.Failure(errorMessage);
        }
    }
}
