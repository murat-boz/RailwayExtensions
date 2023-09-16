using System;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Execute <paramref name="func"/> in try catch only if failure
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <param name="aggregateErrorMessages">Aggregates all failure messages and exceptions</param>
        /// <param name="errorHandler">Handler method when an exception occurred</param>
        /// <returns>Return new failure <see cref="Result{TOut}"/> if not getting an error while <paramref name="func"/> is invoked. 
        /// If getting an error while <paramref name="func"/> is invoked, then looking up the <paramref name="aggregateErrorMessages"/>.
        /// if it is true, return results combined incoming <paramref name="result"/> with get an error from <paramref name="func"/>,
        /// otherwise return new failure <see cref="Result{TOut}"/> decorated with <paramref name="result"/> values</returns>
        public async static Task<Result<TOut>> OnFailureTryAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<TOut>> func,
            bool aggregateErrorMessages = true,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsSuccess)
            {
                return Result.Ok<TOut>(default(TOut));
            }

            var tryResult = await Result.TryAsync(async () => await func(result.Value), errorHandler);

            if (tryResult.IsSuccess)
            {
                return Result.Failure<TOut>(tryResult.Value, result.Error, result.Exception);
            }

            var combineResult = Result.Combine(aggregateErrorMessages, result, tryResult);

            return Result.Failure<TOut>(combineResult.Error, combineResult.Exception);
        }

        /// <summary>
        /// Execute <paramref name="func"/> in try catch only if failure
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <param name="aggregateErrorMessages">Aggregates all failure messages and exceptions</param>
        /// <param name="errorHandler">Handler method when an exception occurred</param>
        /// <returns>Return new failure <see cref="Result{TOut}"/> if not getting an error while <paramref name="func"/> is invoked. 
        /// If getting an error while <paramref name="func"/> is invoked, then looking up the <paramref name="aggregateErrorMessages"/>.
        /// if it is true, return results combined incoming <paramref name="result"/> with get an error from <paramref name="func"/>,
        /// otherwise return new failure <see cref="Result{TOut}"/> decorated with <paramref name="result"/> values</returns>
        public async static Task<Result<TOut>> OnFailureTryAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Task<TOut>> func,
            bool aggregateErrorMessages = true,
            Func<Exception, string> errorHandler = null)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.OnFailureTryAsync<TIn, TOut>(func, aggregateErrorMessages, errorHandler);
        }

        
        public async static Task<Result> OnFailureTryAsync<T>(
            this Result result,
            Func<Task<T>> func,
            bool aggregateErrorMessages = true,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsSuccess)
            {
                return result;
            }

            var tryResult = await Result.TryAsync(func, errorHandler);

            if (tryResult.IsSuccess)
            {
                return result;
            }

            var combineResult = Result.Combine(aggregateErrorMessages, result, tryResult);

            return Result.Failure(combineResult.Error, combineResult.Exception);
        }

        public async static Task<Result> OnFailureTryAsync<T>(
            this Task<Result> resultTask,
            Func<Task<T>> func,
            bool aggregateErrorMessages = true,
            Func<Exception, string> errorHandler = null)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.OnFailureTryAsync<T>(func, aggregateErrorMessages, errorHandler);
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if failure
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if failure, otherwise incoming <paramref name="result"/>></returns>
        public async static Task<Result<T>> OnFailureTryAsync<T>(
            this Result<T> result,
            Func<Task<T>> func,
            bool aggregateErrorMessages = true,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsSuccess)
            {
                return result;
            }

            var tryResult = await Result.TryAsync(func, errorHandler);

            if (tryResult.IsSuccess)
            {
                return result;
            }

            var combineResult = Result.Combine<T>(aggregateErrorMessages, result, tryResult);

            return Result.Failure<T>(result.Value, combineResult.Error, combineResult.Exception);
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if failure
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if failure, otherwise incoming <paramref name="result"/>></returns>
        public async static Task<Result<T>> OnFailureTryAsync<T>(
            this Task<Result<T>> resultTask,
            Func<Task<T>> func,
            bool aggregateErrorMessages = true,
            Func<Exception, string> errorHandler = null)
        {
            var result = await resultTask.ConfigureAwait(false);

            return await result.OnFailureTryAsync<T>(func, aggregateErrorMessages, errorHandler);
        }
    }
}
