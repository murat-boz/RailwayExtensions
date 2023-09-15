using System;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Execute <paramref name="func"/> only if failure
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return new failure <see cref="Result{TOut}"/> if failure, otherwise return new ok <see cref="Result{TOut}"/></returns>
        public async static Task<Result<TOut>> OnFailureAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<TOut>> func)
        {
            if (result.IsFailure)
            {
                return Result.Failure<TOut>(
                    await func(result.Value),
                    result.Error,
                    result.Exception);
            }

            return Result.Ok<TOut>(default(TOut));
        }

        /// <summary>
        /// Execute <paramref name="func"/> only if failure
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return new failure <see cref="Result{TOut}"/> if failure, otherwise return new ok <see cref="Result{TOut}"/></returns>
        public async static Task<Result<TOut>> OnFailureAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Task<TOut>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                return Result.Failure<TOut>(
                    await func(result.Value),
                    result.Error, 
                    result.Exception);
            }

            return Result.Ok<TOut>(default(TOut));
        }

        /// <summary>
        /// Execute <paramref name="func"/> only if failure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if failure, otherwise incoming <paramref name="result"/>></returns>
        public async static Task<Result> OnFailureAsync<T>(
            this Result result,
            Func<Task<T>> func)
        {
            if (result.IsFailure)
            {
                await func();
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="func"/> only if failure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if failure, otherwise incoming <paramref name="result"/>></returns>
        public async static Task<Result> OnFailureAsync<T>(
            this Task<Result> resultTask,
            Func<Task<T>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                await func();
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if failure
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if failure, otherwise incoming <paramref name="result"/>></returns>
        public async static Task<Result<T>> OnFailureAsync<T>(
            this Result<T> result,
            Func<Task<T>> func)
        {
            if (result.IsFailure)
            {
                await func();
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if failure
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if failure, otherwise incoming <paramref name="result"/>></returns>
        public async static Task<Result<T>> OnFailureAsync<T>(
            this Task<Result<T>> resultTask,
            Func<Task<T>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                await func();
            }

            return result;
        }
    }
}
