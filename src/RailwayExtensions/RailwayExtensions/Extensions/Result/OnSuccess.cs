﻿using System;

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
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static Result OnSuccess(
            this Result result,
            Func<Result> func)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return func();
        }
    }
}
