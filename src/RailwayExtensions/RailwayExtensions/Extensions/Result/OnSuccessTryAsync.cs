using System;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Execute <paramref name="func"/> only if success
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return new failure <see cref="Result{TOut}"/> if failure, otherwise return new ok <see cref="Result{TOut}"/></returns>
        public static async Task<Result<TOut>> OnSuccessTryAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<TOut>> func,
            Func<Exception, string> errorHandler = null)
        {
            return result.IsFailure
                ? Result.Failure<TOut>(result.Error, result.Exception)
                : await Result.TryAsync<TOut>(async () => await func(result.Value), errorHandler);
        }

        /// <summary>
        /// Execute <paramref name="func"/> only if success
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return new failure <see cref="Result{TOut}"/> if failure, otherwise return new ok <see cref="Result{TOut}"/></returns>
        public static async Task<Result<TOut>> OnSuccessTryAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Task<TOut>> func,
            Func<Exception, string> errorHandler = null)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.OnSuccessTryAsync<TIn, TOut>(func, errorHandler);
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static async Task<Result> OnSuccessTryAsync<T>(
            this Result result,
            Func<Task<T>> func,
            Func<Exception, string> errorHandler = null)
        {
            var tryResult = await Result.TryAsync(async() => await func(), errorHandler);

            return tryResult.IsSuccess
                ? Result.Ok(tryResult)
                : Result.Failure(tryResult.Error);
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static async Task<Result> OnSuccessTryAsync<T>(
            this Task<Result> resultTask,
            Func<Task<T>> func,
            Func<Exception, string> errorHandler = null)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.OnSuccessTryAsync<T>(func, errorHandler);
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static async Task<Result<T>> OnSuccessTryAsync<T>(
            this Result<T> result,
            Func<Task<T>> func,
            Func<Exception, string> errorHandler = null)
        {
            return result.IsSuccess
                ? await Result.TryAsync<T>(async() => await func(), errorHandler)
                : result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static async Task<Result<T>> OnSuccessTryAsync<T>(
            this Task<Result<T>> resultTask,
            Func<Task<T>> func,
            Func<Exception, string> errorHandler = null)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.OnSuccessTryAsync<T>(func, errorHandler);
        }
    }
}
