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
        public static async Task<Result<TOut>> OnSuccessAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<TOut>> func)
        {
            if (result.IsFailure)
            {
                return Result.Failure<TOut>(result.Error, result.Exception);
            }

            return Result.Ok(await func(result.Value));
        }

        /// <summary>
        /// Execute <paramref name="func"/> only if success
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return new failure <see cref="Result{TOut}"/> if failure, otherwise return new ok <see cref="Result{TOut}"/></returns>
        public static async Task<Result<TOut>> OnSuccessAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Task<TOut>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Failure<TOut>(result.Error, result.Exception);
            }

            return Result.Ok(await func(result.Value));
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static async Task<Result> OnSuccessAsync<T>(
            this Result result,
            Func<Task<T>> func)
        {
            if (result.IsSuccess)
            {
                await func();
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static async Task<Result> OnSuccessAsync<T>(
            this Task<Result> resultTask,
            Func<Task<T>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsSuccess)
            {
                await func();
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static async Task<Result<T>> OnSuccessAsync<T>(
            this Result<T> result,
            Func<Task<T>> func)
        {
            if (result.IsSuccess)
            {
                await func();
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static async Task<Result<T>> OnSuccessAsync<T>(
            this Task<Result<T>> resultTask,
            Func<Task<T>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsSuccess)
            {
                await func();
            }

            return result;
        }
    }
}
