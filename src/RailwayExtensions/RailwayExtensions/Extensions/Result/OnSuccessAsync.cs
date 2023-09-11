using System;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public async static Task<Result<T>> OnSuccessAsync<T>(
            this Result<T> result,
            Action<T> action)
        {
            return result.OnSuccess(action);
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result{T}"/></returns>
        public async static Task<Result<T>> OnSuccessAsync<T>(
            this Task<Result<T>> resultTask,
            Action<T> action)
        {
            Result<T> result = await resultTask.ConfigureAwait(false);

            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        public async static Task<Result> OnSuccessAsync(
            this Result result,
            Action action)
        {
            return result.OnSuccess(action);
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result"/></returns>
        public async static Task<Result> OnSuccessAsync(
            this Task<Result> resultTask,
            Action action)
        {
            Result result = await resultTask.ConfigureAwait(false);

            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        public async static Task<Result<TOut>> OnSuccessAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> func)
        {
            return result.OnSuccess(func);
        }

        public async static Task<Result<TOut>> OnSuccessAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, TOut> func)
        {
            Result<TIn> result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess 
                ? Result.Ok(func(result.Value))
                : Result.Failure<TOut>(result.Error, result.Exception);
        }

        public async static Task<Result<TOut>> OnSuccessAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<TOut>> func)
        {
            return
                await Result.CreateAsync(result.Value)
                            .OnSuccessAsync(async x => await func(x));
        }

        /// <summary>
        /// Execute <paramref name="func"/> only if success
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return new failure <see cref="Result{TOut}"/> if failure, otherwise return new ok <see cref="Result{TOut}"/></returns>
        public async static Task<Result<TOut>> OnSuccessAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Task<TOut>> func)
        {
            Result<TIn> result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess 
                ? Result.Ok(await func(result.Value))
                : Result.Failure<TOut>(result.Error, result.Exception);
        }

        public async static Task<Result> OnSuccessAsync(
            this Result result,
            Func<Result> func)
        {
            return result.OnSuccess(func);
        }

        public async static Task<Result> OnSuccessAsync(
            this Task<Result> resultTask,
            Func<Result> func)
        {
            Result result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess 
                ? func() 
                : result;
        }

        public async static Task<Result> OnSuccessAsync(
            this Result result,
            Func<Task<Result>> func)
        {
            return
                await Result.CreateAsync(result)
                            .OnSuccessAsync(async () => await func());
        }

        /// <summary>
        /// Execute <paramref name="func"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public async static Task<Result> OnSuccessAsync(
            this Task<Result> resultTask,
            Func<Task<Result>> func)
        {
            Result result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? await func()
                : result;
        }
    }
}
