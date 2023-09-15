using System;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Execute <paramref name="func"/> in try catch function only if success
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return new failure <see cref="Result{TOut}"/> if failure, otherwise return new ok <see cref="Result{TOut}"/></returns>
        public static Result<TOut> OnSuccessTry<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> func, 
            Func<Exception, string> errorHandler = null)
        {
            return result.IsFailure 
                ? Result.Failure<TOut>(result.Error, result.Exception) 
                : Result.Try(
                    () => func(result.Value), 
                    errorHandler);
        }

        /// <summary>
        /// Execute <paramref name="func"/> in try catch function only if success
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return new failure <see cref="Result{TOut}"/> if failure, otherwise return new ok <see cref="Result{TOut}"/></returns>
        public static Result<TOut> OnSuccessTry<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, TOut> func,
            Func<Exception, string> errorHandler = null)
        {
            var result = resultTask.Result;

            return result.OnSuccessTry(func, errorHandler);
        }

        /// <summary>
        /// Execute <paramref name="action"/> in try catch function only if success
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result{T}"/></returns>
        public static Result<T> OnSuccessTry<T>(
            this Result<T> result,
            Action<T> action,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsFailure)
            {
                return result;
            }

            var tryResult = Result.Try(() => action(result.Value), errorHandler);

            return tryResult.IsSuccess 
                ? Result.Ok<T>(result.Value) 
                : Result.Failure<T>(tryResult.Error, tryResult.Exception);
        }

        /// <summary>
        /// Execute <paramref name="action"/> in try catch function only if success
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result{T}"/></returns>
        public static Result<T> OnSuccessTry<T>(
            this Task<Result<T>> resultTask,
            Action<T> action,
            Func<Exception, string> errorHandler = null)
        {
            var result = resultTask.Result;

            return result.OnSuccessTry(action, errorHandler);
        }

        /// <summary>
        /// Execute <paramref name="action"/> in try catch function only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result"/></returns>
        public static Result OnSuccessTry(
            this Result result,
            Action action,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return Result.Try(action, errorHandler);
        }

        /// <summary>
        /// Execute <paramref name="action"/> in try catch function only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result"/></returns>
        public static Result OnSuccessTry(
            this Task<Result> resultTask,
            Action action,
            Func<Exception, string> errorHandler = null)
        {
            var result = resultTask.Result;

            return result.OnSuccessTry(action, errorHandler);
        }

        /// <summary>
        /// Execute <paramref name="action"/> in try catch function only if success
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static Result OnSuccessTry<T>(
            this Result result,
            Func<T> func,
            Func<Exception, string> errorHandler = null)
        {
            return result.IsFailure 
                ? result 
                : Result.Try(func, errorHandler);
        }

        /// <summary>
        /// Execute <paramref name="action"/> in try catch function only if success
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static Result OnSuccessTry<T>(
            this Task<Result> resultTask,
            Func<T> func,
            Func<Exception, string> errorHandler = null)
        {
            var result = resultTask.Result;

            return result.OnSuccessTry<T>(func, errorHandler);
        }

        /// <summary>
        /// Execute <paramref name="action"/> in try catch function only if success
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static Result<T> OnSuccessTry<T>(
            this Result<T> result,
            Func<T> func,
            Func<Exception, string> errorHandler = null)
        {
            return result.IsFailure
                ? result
                : Result.Try(func, errorHandler);
        }

        /// <summary>
        /// Execute <paramref name="action"/> in try catch function only if success
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static Result<T> OnSuccessTry<T>(
            this Task<Result<T>> resultTask,
            Func<T> func,
            Func<Exception, string> errorHandler = null)
        {
            var result = resultTask.Result;

            return result.OnSuccessTry(func, errorHandler);
        }
    }
}
