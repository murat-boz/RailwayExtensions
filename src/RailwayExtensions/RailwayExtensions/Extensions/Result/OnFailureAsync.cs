using System;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public async static Task<Result<T>> OnFailureAsync<T>(
            this Result<T> result,
            Action<T> action)
        {
            return result.OnFailure(action);
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result{T}"/></returns>
        public async static Task<Result<T>> OnFailureAsync<T>(
            this Task<Result<T>> resultTask,
            Action<T> action)
        {
            Result<T> result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                action(result.Value);
            }

            return result;
        }

        public async static Task<Result> OnFailureAsync(
            this Result result,
            Action action)
        {
            return result.OnFailure(action);
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result"/></returns>
        public async static Task<Result> OnFailureAsync(
            this Task<Result> resultTask,
            Action action)
        {
            Result result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public async static Task<Result<TOut>> OnFailureAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> func)
        {
            return result.OnFailure(func);
        }

        public async static Task<Result<TOut>> OnFailureAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, TOut> func)
        {
            Result<TIn> result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                func(result.Value);

                return Result.Failure<TOut>(result.Error);
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
            this Result<TIn> result,
            Func<TIn, Task<TOut>> func)
        {
            return
                await Result.CreateAsync(result.Value)
                            .OnFailureAsync(async x => await func(x));
        }

        /// <summary>
        /// Execute <paramref name="func"/> only if success
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
            Result<TIn> result = await resultTask.ConfigureAwait(false);

            if (result.IsFailure)
            {
                await func(result.Value);

                return Result.Failure<TOut>(result.Error);
            }

            return Result.Ok<TOut>(default(TOut));
        }

        public async static Task<Result> OnFailureAsync(
            this Result result,
            Func<Result> func)
        {
            return result.OnFailure(func);
        }

        public async static Task<Result> OnFailureAsync(
            this Task<Result> resultTask,
            Func<Result> func)
        {
            Result result = await resultTask.ConfigureAwait(false);

            return result.IsFailure
                ? func()
                : result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if failure
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if failure, otherwise incoming <paramref name="result"/>></returns>
        public async static Task<Result> OnFailureAsync(
            this Result result,
            Func<Task<Result>> func)
        {
            return
                await Result.CreateAsync(result)
                            .OnFailureAsync(async () => await func());
        }

        /// <summary>
        /// Execute <paramref name="func"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public async static Task<Result> OnFailureAsync(
            this Task<Result> resultTask,
            Func<Task<Result>> func)
        {
            Result result = await resultTask.ConfigureAwait(false);

            return result.IsFailure
                ? await func()
                : result;
        }
    }
}
