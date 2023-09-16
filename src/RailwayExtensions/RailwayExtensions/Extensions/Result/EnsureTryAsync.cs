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
        public async static Task<Result<T>> EnsureTryAsync<T>(
            this Result<T> result,
            Func<T, Task<bool>> predicate,
            string errorMessage,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsFailure)
            {
                return result;
            }

            var tryResult = await Result.TryAsync(async() => await predicate(result.Value), errorHandler);

            return tryResult.IsSuccess
                ? result
                : Result.Failure<T>(errorMessage);
        }

        public async static Task<Result<T>> EnsureTryAsync<T>(
            this Task<Result<T>> resultTask,
            Func<T, Task<bool>> predicate,
            string errorMessage,
            Func<Exception, string> errorHandler = null)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.EnsureTryAsync(predicate, errorMessage, errorHandler);
        }

        public async static Task<Result> EnsureTryAsync(
            this Result result,
            Func<Task<bool>> predicate,
            string errorMessage,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsFailure)
            {
                return result;
            }

            var tryResult = await Result.TryAsync(predicate, errorHandler);

            return tryResult.IsSuccess
                ? result
                : Result.Failure(errorMessage);
        }

        public async static Task<Result> EnsureTryAsync(
            this Task<Result> resultTask,
            Func<Task<bool>> predicate,
            string errorMessage,
            Func<Exception, string> errorHandler = null)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.EnsureTryAsync(predicate, errorMessage, errorHandler);
        }
    }
}
