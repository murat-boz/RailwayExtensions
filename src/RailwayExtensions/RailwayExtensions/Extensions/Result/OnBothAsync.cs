using System;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public async static Task<Result> OnBothAsync(this Result result, Func<string, Task<Result>> func, string message)
        {
            await func(message);

            return result;
        }

        public async static Task<Result> OnBothAsync(this Task<Result> result, Func<string, Task<Result>> func, string message)
        {
            await func(message);

            return await result;
        }

        public async static Task<Result<T>> OnBothAsync<T>(this Result<T> result, Func<Result, Task<T>> func)
        {
            await func(result);

            return result;
        }

        public async static Task<Result<T>> OnBothAsync<T>(this Task<Result<T>> resultTask, Func<Result, Task<T>> func)
        {
            var result = await resultTask;

            await func(result);

            return result;
        }

        public async static Task<Result> OnBothAsync(this Result result, Action action)
        {
            return result.OnBoth(action);
        }

        public async static Task<Result> OnBothAsync(this Task<Result> resultTask, Action action)
        {
            action();

            return await resultTask;
        }

        public async static Task<Result> OnBothAsync(this Result result, Action<Result> action)
        {
            action(result);

            return result;
        }

        public async static Task<Result> OnBothAsync(this Task<Result> resultTask, Action<Result> action)
        {
            var result = await resultTask;

            action(result);

            return result;
        }

        public async static Task<Result> OnBothAsync(this Result result, Action<string> action, string message)
        {
            action(message);

            return result;
        }

        public static async Task<Result> OnBothAsync(this Task<Result> resultTask, Action<string> action, string message)
        {
            var result = await resultTask;

            action(message);

            return result;
        }

        public async static Task<Result<T>> OnBothAsync<T>(this Result<T> result, Action<T> action)
        {
            action(result.Value);

            return result;
        }

        public async static Task<Result<T>> OnBothAsync<T>(this Task<Result<T>> resultTask, Action<T> action)
        {
            var result = await resultTask;

            action(result.Value);

            return result;
        }
    }
}
