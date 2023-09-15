using System;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="predicate"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
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

        public static Result<T> Ensure<T>(
            this Task<Result<T>> resultTask,
            Func<T, bool> predicate,
            string errorMessage)
        {
            var result = resultTask.Result;

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

        public static Result Ensure(
            this Task<Result> resultTask,
            Func<bool> predicate,
            string errorMessage)
        {
            var result = resultTask.Result;

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
