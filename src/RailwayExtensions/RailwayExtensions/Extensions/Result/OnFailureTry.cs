using System;

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
        /// <returns>Return new failure <see cref="Result{TOut}"/> if failure, otherwise return new ok <see cref="Result{TOut}"/></returns>
        public static Result<TOut> OnFailureTry<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> func,
            bool aggregateErrorMessages = true,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsSuccess)
            {
                return Result.Ok<TOut>(default(TOut));
            }

            var tryResult = Result.Try(() => func(result.Value), errorHandler);

            if (tryResult.IsSuccess)
            {
                return Result.Failure<TOut>(tryResult.Value, result.Error, result.Exception);
            }
            else
            {
                var combineResult = Result.Combine(aggregateErrorMessages, result, tryResult);

                return Result.Failure<TOut>(combineResult.Error, combineResult.Exception);
            }
        }

        /// <summary>
        /// Execute <paramref name="action"/> in try catch only if failure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <param name="aggregateErrorMessages">Aggregates all failure messages and exceptions</param>
        /// <param name="errorHandler">Handler method when an exception occurred</param>
        /// <returns>Return incoming <see cref="Result{T}"/></returns>
        public static Result<T> OnFailureTry<T>(
            this Result<T> result,
            Action<T> action,
            bool aggregateErrorMessages = true,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsSuccess)
            {
                return result;
            }

            var tryResult = Result.Try(() => action(result.Value), errorHandler);

            if (tryResult.IsSuccess)
            {
                return result;
            }
            else
            {
                var combineResult = Result.Combine(aggregateErrorMessages, result, tryResult);

                return Result.Failure<T>(result.Value, combineResult.Error, combineResult.Exception);
            }
        }

        /// <summary>
        /// Execute <paramref name="action"/> in try catch only if failure
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <param name="aggregateErrorMessages">Aggregates all failure messages and exceptions</param>
        /// <param name="errorHandler">Handler method when an exception occurred</param>
        /// <returns></returns>
        public static Result OnFailureTry(
            this Result result,
            Action action,
            bool aggregateErrorMessages = true,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsSuccess)
            {
                return result;
            }

            var tryResult = Result.Try(() => action(), errorHandler);

            if (tryResult.IsSuccess)
            {
                return result;
            }
            else
            {
                var combineResult = Result.Combine(aggregateErrorMessages, result, tryResult);

                return Result.Failure(combineResult.Error, combineResult.Exception);
            }
        }

        /// <summary>
        /// Execute <paramref name="func"/> in try catch only if failure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <param name="aggregateErrorMessages">Aggregates all failure messages and exceptions</param>
        /// <param name="errorHandler">Handler method when an exception occurred</param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if failure, otherwise incoming <paramref name="result"/>></returns>
        public static Result OnFailureTry<T>(
            this Result result,
            Func<T> func,
            bool aggregateErrorMessages = true,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsSuccess)
            {
                return result;
            }

            var tryResult = Result.Try(() => func(), errorHandler);

            if (tryResult.IsSuccess)
            {
                return result;
            }
            else
            {
                var combineResult = Result.Combine(aggregateErrorMessages, result, tryResult);

                return Result.Failure(combineResult.Error, combineResult.Exception);
            }
        }

        /// <summary>
        /// Execute <paramref name="func"/> in try catch only if failure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <param name="aggregateErrorMessages">Aggregates all failure messages and exceptions</param>
        /// <param name="errorHandler">Handler method when an exception occurred</param>
        /// <returns>Return <see cref="Result{T}"/> of processed on <paramref name="func"/> if failure, otherwise incoming <paramref name="result"/>></returns>
        public static Result<T> OnFailureTry<T>(
            this Result<T> result,
            Func<T> func,
            bool aggregateErrorMessages = true,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsSuccess)
            {
                return result;
            }

            var tryResult = Result.Try(() => func(), errorHandler);

            if (tryResult.IsSuccess)
            {
                return result;
            }
            else
            {
                var combineResult = Result.Combine<T>(aggregateErrorMessages, result, tryResult);

                return Result.Failure<T>(combineResult.Error, combineResult.Exception);
            }
        }
    }
}
