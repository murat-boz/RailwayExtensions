using System;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public static async Task<Result> OnBothTryAsync(
            this Result result, 
            Func<Task> func,
            Func<Exception, string> errorHandler = null)
        {
            var tryResult = await Result.TryAsync(func, errorHandler);

            return tryResult.IsSuccess
                ? result
                : tryResult;
        }

        public static async Task<Result> OnBothTryAsync(
            this Task<Result> resultTask, 
            Func<Task> func,
            Func<Exception, string> errorHandler = null)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.OnBothTryAsync(func, errorHandler);
        }

        public static async Task<Result> OnBothTryAsync(
            this Result result, 
            Func<string, Task> func, 
            string message,
            Func<Exception, string> errorHandler = null)
        {
            var tryResult = await Result.TryAsync(async () => await func(message), errorHandler);

            return tryResult.IsSuccess
                ? result 
                : tryResult;
        }

        public static async Task<Result> OnBothTryAsync(
            this Task<Result> resultTask, 
            Func<string, Task> func, 
            string message,
            Func<Exception, string> errorHandler = null)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.OnBothTryAsync(func, message, errorHandler);
        }

        public static async Task<Result<T>> OnBothTryAsync<T>(
            this Result<T> result, 
            Func<Task<T>> func,
            Func<Exception, string> errorHandler = null)
        {
            var tryResult = await Result.TryAsync<T>(func, errorHandler);

            return tryResult.IsSuccess
                ? result 
                : tryResult;
        }

        public static async Task<Result<T>> OnBothTryAsync<T>(
            this Task<Result<T>> resultTask,
            Func<Task<T>> func,
            Func<Exception, string> errorHandler = null)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.OnBothTryAsync(func, errorHandler);
        }

        public static async Task<Result<TOut>> OnBothTryAsync<TIn, TOut>(
            this Result<TIn> result, 
            Func<TIn, Task<TOut>> func,
            Func<Exception, string> errorHandler = null)
        {
            var tryResult = await Result.TryAsync(async () => await func(result.Value), errorHandler);

            return tryResult.IsSuccess
                ? Result.Map<TOut>(await func(result.Value), result)
                : tryResult;
        }

        public static async Task<Result<TOut>> OnBothTryAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask, 
            Func<TIn, Task<TOut>> func,
            Func<Exception, string> errorHandler = null)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.OnBothTryAsync(func, errorHandler);
        }
    }
}
