using System.Threading.Tasks;
using System;

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
        public static Result<T> EnsureTry<T>(
            this Result<T> result,
            Func<T, bool> predicate,
            string errorMessage,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsFailure)
            {
                return result;
            }

            var tryPredicate = Result.Try(() => predicate(result.Value), errorHandler);

            return tryPredicate.Value
                ? result
                : Result.Failure<T>(errorMessage);
        }

        public static Result<T> EnsureTry<T>(
            this Task<Result<T>> resultTask,
            Func<T, bool> predicate,
            string errorMessage,
            Func<Exception, string> errorHandler = null)
        {
            var result = resultTask.Result;

            return result.EnsureTry(predicate, errorMessage, errorHandler);
        }

        public static Result EnsureTry(
            this Result result,
            Func<bool> predicate,
            string errorMessage,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsFailure)
            {
                return result;
            }

            var tryPredicate = Result.Try(predicate, errorHandler);

            return tryPredicate.Value
                ? result
                : Result.Failure(errorMessage);
        }

        public static Result EnsureTry(
            this Task<Result> resultTask,
            Func<bool> predicate,
            string errorMessage,
            Func<Exception, string> errorHandler = null)
        {
            var result = resultTask.Result;

            return result.EnsureTry(predicate, errorMessage, errorHandler);
        }
    }
}
