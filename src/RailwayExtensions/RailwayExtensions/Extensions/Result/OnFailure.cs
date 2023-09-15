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
        public static Result<TOut> OnFailure<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> func)
        {
            if (result.IsFailure)
            {
                return Result.Failure<TOut>(
                    func(result.Value), 
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
        public static Result<TOut> OnFailure<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, TOut> func)
        {
            var result = resultTask.Result;

            return result.OnFailure<TIn, TOut>(func);
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if failure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result{T}"/></returns>
        public static Result<T> OnFailure<T>(
            this Result<T> result,
            Action<T> action)
        {
            if (result.IsFailure)
            {
                action(result.Value);
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if failure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result{T}"/></returns>
        public static Result<T> OnFailure<T>(
            this Task<Result<T>> resultTask,
            Action<T> action)
        {
            var result = resultTask.Result;

            return result.OnFailure(action);
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if failure
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result"/></returns>
        public static Result OnFailure(
            this Result result, 
            Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if failure
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result"/></returns>
        public static Result OnFailure(
            this Task<Result> resultTask,
            Action action)
        {
            var result = resultTask.Result;

            return result.OnFailure(action);
        }

        /// <summary>
        /// Execute <paramref name="func"/> only if failure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if failure, otherwise incoming <paramref name="result"/>></returns>
        public static Result OnFailure<T>(
            this Result result,
            Func<T> func)
        {
            if (result.IsFailure)
            {
                func();
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
        public static Result OnFailure<T>(
            this Task<Result> resultTask,
            Func<T> func)
        {
            var result = resultTask.Result;

            return result.OnFailure<T>(func);
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if failure
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if failure, otherwise incoming <paramref name="result"/>></returns>
        public static Result<T> OnFailure<T>(
            this Result<T> result,
            Func<T> func)
        {
            if (result.IsFailure)
            {
                func();
            }

            return result;
        }

        /// <summary>
        /// Execute <paramref name="action"/> only if failure
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if failure, otherwise incoming <paramref name="result"/>></returns>
        public static Result<T> OnFailure<T>(
            this Task<Result<T>> resultTask,
            Func<T> func)
        {
            var result = resultTask.Result;

            return result.OnFailure(func);
        }
    }
}
