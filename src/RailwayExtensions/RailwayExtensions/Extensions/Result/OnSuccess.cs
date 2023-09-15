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
        public static Result<TOut> OnSuccess<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> func)
        {
            if (result.IsFailure)
            {
                return Result.Failure<TOut>(result.Error, result.Exception);
            }

            return Result.Ok(func(result.Value));
        }

        /// <summary>
        /// Execute <paramref name="func"/> only if success
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return new failure <see cref="Result{TOut}"/> if failure, otherwise return new ok <see cref="Result{TOut}"/></returns>
        public static Result<TOut> OnSuccess<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, TOut> func)
        {
            var result = resultTask.Result;

            if (result.IsFailure)
            {
                return Result.Failure<TOut>(result.Error, result.Exception);
            }

            return Result.Ok(func(result.Value));
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result{T}"/></returns>
        public static Result<T> OnSuccess<T>(
            this Result<T> result, 
            Action<T> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result{T}"/></returns>
        public static Result<T> OnSuccess<T>(
            this Task<Result<T>> resultTask,
            Action<T> action)
        {
            var result = resultTask.Result;

            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result"/></returns>
        public static Result OnSuccess(
            this Result result, 
            Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result"/></returns>
        public static Result OnSuccess(
            this Task<Result> resultTask,
            Action action)
        {
            var result = resultTask.Result;

            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static Result OnSuccess<T>(
            this Result result,
            Func<T> func)
        {
            if (result.IsSuccess)
            {
                func();
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static Result OnSuccess<T>(
            this Task<Result> resultTask,
            Func<T> func)
        {
            var result = resultTask.Result;

            if (result.IsSuccess)
            {
                func();
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static Result<T> OnSuccess<T>(
            this Result<T> result,
            Func<T> func)
        {
            if (result.IsSuccess)
            {
                func();
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static Result<T> OnSuccess<T>(
            this Task<Result<T>> resultTask,
            Func<T> func)
        {
            var result = resultTask.Result;

            if (result.IsSuccess)
            {
                func();
            }

            return result;
        }
    }
}
