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
        public async static Task<Result<T>> EnsureAsync<T>(
            this Result<T> result,
            Func<T, Task<bool>> predicate,
            string errorMessage)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return await predicate(result.Value)
                ? result
                : Result.Failure<T>(errorMessage);
        }

        public async static Task<Result<T>> EnsureAsync<T>(
            this Task<Result<T>> resultTask,
            Func<T, Task<bool>> predicate,
            string errorMessage)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.EnsureAsync(predicate, errorMessage);
        }

        public async static Task<Result> EnsureAsync(
            this Result result,
            Func<Task<bool>> predicate,
            string errorMessage)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return await predicate()
                ? result
                : Result.Failure(errorMessage);
        }

        public async static Task<Result> EnsureAsync(
            this Task<Result> resultTask,
            Func<Task<bool>> predicate,
            string errorMessage)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.EnsureAsync(predicate, errorMessage);
        }
    }
}
