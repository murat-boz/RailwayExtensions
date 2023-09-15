using System;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public static async Task<Result> OnBothAsync(this Result result, Func<Task> func)
        {
            await func();

            return result;
        }

        public static async Task<Result> OnBothAsync(this Task<Result> resultTask, Func<Task> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.OnBothAsync(func);
        }

        public static async Task<Result> OnBothAsync(this Result result, Func<string, Task> func, string message)
        {
            await func(message);

            return result;
        }

        public static async Task<Result> OnBothAsync(this Task<Result> resultTask, Func<string, Task> func, string message)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.OnBothAsync(func, message);
        }

        public static async Task<Result<T>> OnBothAsync<T>(this Result<T> result, Func<Task<T>> func)
        {
            await func();

            return result;
        }

        public static async Task<Result<T>> OnBothAsync<T>(this Task<Result<T>> resultTask, Func<Task<T>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.OnBothAsync(func);
        }

        public static async Task<Result<TOut>> OnBothAsync<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<TOut>> func)
        {
            return Result.Map<TOut>(await func(result.Value), result);
        }

        public static async Task<Result<TOut>> OnBothAsync<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, Task<TOut>> func)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.OnBothAsync(func);
        }
    }
}
