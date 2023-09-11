using System;

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
                func(result.Value);
                return Result.Failure<TOut>(result.Error, result.Exception);
            }

            return Result.Ok<TOut>(default(TOut));
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
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if failure, otherwise incoming <paramref name="result"/>></returns>
        public static Result OnFailure(
            this Result result,
            Func<Result> func)
        {
            return result.IsFailure 
                        ? func() 
                        : result;
        }
    }
}
